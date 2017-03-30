namespace MsmqTester.Commands
{
    public interface ICommand
    {
        void Execute();

        bool HadErrorInCreation();
    }
}