using System;

namespace MsmqTester
{
    internal class Program
    {
        private static readonly string LINE_PREFIX = "MsmqTester:> ";

        private static int _messagesRetrieved;

        private static void Main(string[] args)
        {
            SettingsProvider.SetToDefault();

            string command;
            bool quitNow = false;
            while (!quitNow)
            {
                Console.Write(LINE_PREFIX);
                command = Console.ReadLine();
                string[] commandSplit = command?.Split(' ');

                switch (commandSplit[0])
                {
                    case "/help":
                    case "/h":
                        InformationProvider.HelpOutput();
                        break;

                    case "/version":
                    case "/v":
                        InformationProvider.VersionNumber();
                        break;

                    case "/clear":
                        Console.Clear();
                        break;

                    case "/quit":
                    case "/q":
                        quitNow = true;
                        break;

                    case "/post":
                    case "/p":
                        QueueHandler.PostMessages(commandSplit);
                        break;

                    case "/retrieve":
                    case "/r":
                        QueueHandler.RetrieveMessages(commandSplit);
                        break;

                    case "/settings":
                        SettingsProvider.ProcessSetting(commandSplit);
                        break;

                    default:
                        Console.WriteLine(InformationProvider.MESSAGE_NOT_RECOGNIZED);
                        Console.WriteLine("");
                        break;
                }
            }
        }
    }
}