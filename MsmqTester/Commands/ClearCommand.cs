using System;

namespace MsmqTester.Commands
{
    public class ClearCommand : ICommand
    {
        public void Execute()
        {
            Console.Clear();
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}