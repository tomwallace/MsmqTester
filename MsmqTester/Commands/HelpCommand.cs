using System;

namespace MsmqTester.Commands
{
    public class HelpCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("The MSMQ Tester offers the following commands");
            Console.WriteLine("");

            Console.WriteLine("/clear");
            Console.WriteLine("Clears the current console window.");
            Console.WriteLine("");

            Console.WriteLine("/help");
            Console.WriteLine("/h");
            Console.WriteLine("Displays the help menu.");
            Console.WriteLine("");

            Console.WriteLine("/post [x]");
            Console.WriteLine("/p [x]");
            Console.WriteLine("Posts x number of messages to the MSMQ server and provides the time it takes to do so.");
            Console.WriteLine("If x is not provided, then posts one message.");
            Console.WriteLine("");

            Console.WriteLine("/quit");
            Console.WriteLine("/q");
            Console.WriteLine("Exits the program.");
            Console.WriteLine("");

            Console.WriteLine("/retrieve [x]");
            Console.WriteLine("/r [x]");
            Console.WriteLine("Retrieves x number of messages to the MSMQ server and provides the time it takes to do so.");
            Console.WriteLine("If x is not provided, then retrieves one message.");
            Console.WriteLine("");

            Console.WriteLine("/settings");
            Console.WriteLine("Lists the current settings and their values.");
            Console.WriteLine("");

            Console.WriteLine("/settings length [x]");
            Console.WriteLine("Sets the message length to x, where x is a number between 10 and integer max.");
            Console.WriteLine("");

            Console.WriteLine("/settings persist [x]");
            Console.WriteLine("Sets the persistance level to x, where x is true or false.");
            Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}