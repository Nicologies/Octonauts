using System;
using coreArgs;

namespace Octonauts.Core
{
    public static class CommandArgsParser
    {
        public static T Parse<T>(string[] args, string additionalHelpText)
        {
            var options = ArgsParser.Parse<T>(args);
            if (options.Errors.Count > 0)
            {
                Console.WriteLine("Failed to parse the arguments");
                Console.WriteLine(ArgsParser.GetHelpText<T>());
                Console.WriteLine(additionalHelpText);
                Environment.Exit(-1);
            }

            return options.Arguments;
        }
    }
}
