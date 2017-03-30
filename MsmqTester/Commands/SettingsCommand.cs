using System;

namespace MsmqTester.Commands
{
    public class SettingsCommand : ICommand
    {
        private readonly string[] _commandSplit;

        public SettingsCommand(string[] commandSplit)
        {
            _commandSplit = commandSplit;
        }

        public void Execute()
        {
            if (_commandSplit.Length == 1)
            {
                SettingsProvider.ListCurrentSettings();
                return;
            }

            switch (_commandSplit[1])
            {
                case "length":
                    SettingsProvider.HandleSettingLength(_commandSplit[2]);
                    break;

                case "persist":
                    SettingsProvider.HandleSettingWillPersist(_commandSplit[2]);
                    break;

                default:
                    Console.WriteLine(SettingsProvider.MESSAGE_NOT_RECOGNIZED);
                    Console.WriteLine("");
                    break;
            }
        }

        public bool HadErrorInCreation()
        {
            return false;
        }
    }
}