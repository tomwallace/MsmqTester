using System;
using System.Configuration;
using System.Messaging;

namespace MsmqTester
{
    public static class QueueHandler
    {
        private static MessageQueue _messageQueue;

        public static void SendMessage(Message message)
        {
            _messageQueue.Send(message);
        }

        public static void InitializeQueue()
        {
            string msmqFullUri = GetMsmqFullUri();
            Console.WriteLine($"Creating MSMQ queue instance for address {msmqFullUri}");

            _messageQueue = new MessageQueue(msmqFullUri)
                .IncludeArrivedTime()
                .IncludeCorrelationId()
                .IncludeLookupId()
                .WithStringFormatter();
        }

        public static void CloseAndDispose()
        {
            _messageQueue.Close();
            _messageQueue.Dispose();
        }

        public static Message Receive()
        {
            return _messageQueue.Receive();
        }

        private static string GetMsmqFullUri()
        {
            string queueName = ConfigurationManager.AppSettings["queueName"];
            string location = ConfigurationManager.AppSettings["msmqLocation"];
            return $"{location}\\{queueName}";
        }
    }
}