using System;
using coreArgs;

namespace Octonauts.Core
{
    public static class CommandArgsParaser
    {
        public static T Parse<T>(string[] args)
        {
            var options = ArgsParser.Parse<T>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<T>());
                Environment.Exit(-1);
            }

            return options.Arguments;
        }
    }
}
