using System;
using MsmqTester.Commands;

namespace MsmqTester
{
    internal class Program
    {
        private static readonly string LINE_PREFIX = "MsmqTester:> ";

        private static void Main(string[] args)
        {
            SettingsProvider.SetToDefault();

            while (true)
            {
                Console.Write(LINE_PREFIX);
                string commandLine = Console.ReadLine();

                CommandBuilder builder = new CommandBuilder();
                ICommand command = builder.Build(commandLine);

                if (!command.HadErrorInCreation())
                {
                    command.Execute();
                }
            }
        }
    }
}