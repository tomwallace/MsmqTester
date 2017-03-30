using System;
using System.Messaging;

namespace MsmqTester.Commands
{
    public class RetrieveCommand : ICommand
    {
        private readonly int _numberOfMessages;
        private readonly bool _errorInCreation;

        public RetrieveCommand(string[] commandSplit)
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
            int messagesRetrieved = 0;

            QueueHandler.InitializeQueue();

            DateTime startTime = DateTime.Now;
            Console.WriteLine($"Starting to retrieve {_numberOfMessages} messages to queue.");

            while (messagesRetrieved < _numberOfMessages)
            {
                Message message = QueueHandler.Receive();
                Console.WriteLine($"Starting to receive message {messagesRetrieved + 1}: {message.Id}");

                string bodyString = (string)message.Body;
                if (bodyString.Length > 50)
                    bodyString = bodyString.Substring(0, 49);

                string bodyStartsWith = $"Message: {bodyString}...";
                Console.WriteLine(bodyStartsWith);

                messagesRetrieved++;
            }

            Console.WriteLine($"Retrieved all {_numberOfMessages} to the queue.");
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