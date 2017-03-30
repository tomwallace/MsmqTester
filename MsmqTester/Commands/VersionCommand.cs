using System;
using System.Configuration;

namespace MsmqTester.Commands
{
    public class VersionCommand : ICommand
    {
        public void Execute()
        {
            string versionNumber = ConfigurationManager.AppSettings["version"];
            Console.WriteLine($"MSMQ Tester version: {versionNumber}");
            Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}