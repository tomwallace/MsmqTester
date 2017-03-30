﻿using System;
using System.Configuration;

namespace MsmqTester
{
    public class SettingsProvider
    {
        public static readonly string MESSAGE_NOT_RECOGNIZED =
            "Command not recognized.  Type /help for more information.";

        // Message settings for tests
        private static bool _willPersist;

        private static int _messageLength;

        public static void SetToDefault()
        {
            // Provide defaults from Configuration
            _willPersist = Convert.ToBoolean(ConfigurationManager.AppSettings["defaultWillPersist"]);
            _messageLength = Convert.ToInt32(ConfigurationManager.AppSettings["defaultMessageLength"]);
        }

        public static bool GetWillPersist()
        {
            return _willPersist;
        }

        public static string GetMessageBody()
        {
            string messagePrefix = $"{_messageLength}charlong";
            string messageSuffix = new String('a', _messageLength - 10);
            return $"{messagePrefix}{messageSuffix}";
        }

        public static void ListCurrentSettings()
        {
            Console.WriteLine("Here are the current settings and their values:");
            Console.WriteLine($"Messages set to persist: {_willPersist}");
            Console.WriteLine($"Message length: {_messageLength}");
            Console.WriteLine("");
        }

        public static void HandleSettingLength(string intAsString)
        {
            int tryConvert;
            if (Int32.TryParse(intAsString, out tryConvert))
            {
                if (tryConvert < 10)
                {
                    IncorrectLength();
                    return;
                }

                _messageLength = tryConvert;
                Console.WriteLine($"Message length set to: {tryConvert}");
                Console.WriteLine("");
                return;
            }

            IncorrectLength();
        }

        public static void HandleSettingWillPersist(string boolAsString)
        {
            if (boolAsString != "true" && boolAsString != "false")
            {
                Console.WriteLine(
                    "Message persistance must be set to either 'true' or 'false'.  You did not provide a setting in this allowable set.  Please try again.");
                Console.WriteLine("");
                return;
            }

            _willPersist = Convert.ToBoolean(boolAsString);
            Console.WriteLine($"Message persistance set to: {boolAsString}");
            Console.WriteLine("");
        }

        private static void IncorrectLength()
        {
            Console.WriteLine("Message length must be between 10 and int max.  You failed to provide a number in this range.  Please try again.");
            Console.WriteLine("");
        }
    }
}