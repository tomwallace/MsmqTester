using System;
using System.Messaging;

namespace MsmqTester.Commands
{
    public class PostCommand : ICommand
    {
        private readonly int _numberOfMessages;
        private readonly bool _errorInCreation;

        public PostCommand(string[] commandSplit)
        {
            int numberOfMessages = 1;
            bool errorInCreation = false;
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
                    errorInCreation = true;
                }
            }
            _numberOfMessages = numberOfMessages;
            _errorInCreation = errorInCreation;
        }

        public void Execute()
        {
            QueueHandler.InitializeQueue();

            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Starting to upload {_numberOfMessages} messages to queue.");

            for (int messagesSent = 1; messagesSent <= _numberOfMessages; messagesSent++)
            {
                Message message = new Message();
                message.Body = SettingsProvider.GetMessageBody();
                message.Label = "Sent from MsmqTester";

                message.TimeToReachQueue = new TimeSpan(0, 5, 0);
                message.UseDeadLetterQueue = true;
                message.Recoverable = SettingsProvider.GetWillPersist();
                message.UseJournalQueue = false;

                QueueHandler.SendMessage(message);
                Console.WriteLine($"Uploaded message {messagesSent}");
            }

            Console.WriteLine($"Uploaded all {_numberOfMessages} to the queue.");
            DateTime endTime = DateTime.Now;
            Console.WriteLine($"Started {startTime} and Ended {endTime}.  Duration = {endTime - startTime}");

            QueueHandler.CloseAndDispose();
        }

        public bool HadErrorInCreation()
        {
            return _errorInCreation;
        }
    }
}