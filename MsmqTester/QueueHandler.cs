using System;
using System.Configuration;
using System.Messaging;

namespace MsmqTester
{
    public static class QueueHandler
    {
        public static void PostMessages(string[] commandSplit)
        {
            int numberOfMessages = 1;
            if (commandSplit.Length > 1)
            {
                int tryConvert;
                if (Int32.TryParse(commandSplit[1], out tryConvert))
                {
                    numberOfMessages = tryConvert;
                }
                else
                {
                    Console.WriteLine("This command requires a number argument and you did not pass one.  Please try again.");
                    return;
                }
            }

            MessageQueue queue = InitializeQueue();

            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Starting to upload {numberOfMessages} messages to queue.");

            for (int messagesSent = 1; messagesSent <= numberOfMessages; messagesSent++)
            {
                Message message = new Message();
                message.Body = SettingsProvider.GetMessageBody();
                message.Label = "Sent from MsmqTester";

                message.TimeToReachQueue = new TimeSpan(0, 5, 0);
                message.UseDeadLetterQueue = true;
                message.Recoverable = SettingsProvider.GetWillPersist();
                message.UseJournalQueue = false;

                queue.Send(message);
                Console.WriteLine($"Uploaded message {messagesSent}");
            }

            Console.WriteLine($"Uploaded all {numberOfMessages} to the queue.");
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Started {startTime} and Ended {endTime}.  Duration = {endTime - startTime}");

            CloseAndDispose(queue);
        }

        public static void RetrieveMessages(string[] commandSplit)
        {
            int messagesRetrieved = 0;

            int numberOfMessages = 1;
            if (commandSplit.Length > 1)
            {
                int tryConvert;
                if (Int32.TryParse(commandSplit[1], out tryConvert))
                {
                    numberOfMessages = tryConvert;
                }
                else
                {
                    Console.WriteLine("This command requires a number argument and you did not pass one.  Please try again.");
                    return;
                }
            }

            MessageQueue queue = InitializeQueue();

            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Starting to retrieve {numberOfMessages} messages to queue.");

            while (messagesRetrieved < numberOfMessages)
            {
                Message message = queue.Receive();
                Console.WriteLine($"Starting to receive message {messagesRetrieved + 1}: {message.Id}");

                string bodyString = (string)message.Body;
                if (bodyString.Length > 50)
                    bodyString = bodyString.Substring(0, 49);

                string bodyStartsWith = $"Message: {bodyString}...";
                Console.WriteLine(bodyStartsWith);

                messagesRetrieved++;
            }

            Console.WriteLine($"Retrieved all {numberOfMessages} to the queue.");
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Started {startTime} and Ended {endTime}.  Duration = {endTime - startTime}");

            CloseAndDispose(queue);
        }

        private static MessageQueue InitializeQueue()
        {
            string msmqFullUri = GetMsmqFullUri();
            Console.WriteLine($"Creating MSMQ queue instance for address {msmqFullUri}");

            return new MessageQueue(msmqFullUri)
                .IncludeArrivedTime()
                .IncludeCorrelationId()
                .IncludeLookupId()
                .WithStringFormatter();
        }

        private static void CloseAndDispose(MessageQueue messageQueue)
        {
            messageQueue.Close();
            messageQueue.Dispose();
        }

        private static string GetMsmqFullUri()
        {
            string queueName = ConfigurationManager.AppSettings["queueName"];
            string location = ConfigurationManager.AppSettings["msmqLocation"];
            return $"{location}\\{queueName}";
        }
    }
}