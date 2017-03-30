using System;

namespace MsmqTester.Commands
{
    public class DefaultCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine(SettingsProvider.MESSAGE_NOT_RECOGNIZED);
            Console.WriteLine("");
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}