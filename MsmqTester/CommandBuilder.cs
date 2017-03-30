using MsmqTester.Commands;

namespace MsmqTester
{
    public class CommandBuilder
    {
        public ICommand Build(string command)
        {
            string[] commandSplit = command?.Split(' ');

            ICommand createdCommand;

            switch (commandSplit[0])
            {
                case "/help":
                case "/h":
                    createdCommand = new HelpCommand();
                    break;

                case "/version":
                case "/v":
                    createdCommand = new VersionCommand();
                    break;

                case "/clear":
                    createdCommand = new ClearCommand();
                    break;

                case "/quit":
                case "/q":
                    createdCommand = new QuitCommand();
                    break;

                case "/post":
                case "/p":
                    createdCommand = new PostCommand(commandSplit);
                    break;

                case "/retrieve":
                case "/r":
                    createdCommand = new RetrieveCommand(commandSplit);
                    break;

                case "/settings":
                    createdCommand = new SettingsCommand(commandSplit);
                    break;

                default:
                    createdCommand = new DefaultCommand();
                    break;
            }

            return createdCommand;
        }
    }
}