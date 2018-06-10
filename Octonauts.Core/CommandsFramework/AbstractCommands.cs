using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class AbstractCommands
    {
        public abstract string Command { get; set; }

        protected abstract Dictionary<string, ICommandHandler> Dispatcher { get; }

        public async Task DispatchCommand<T>(string[] args) where T : AbstractCommands
        {
            var options = CommandArgsParaser.Parse<T>(args);
            if (Dispatcher.TryGetValue(options.Command.ToLowerInvariant(), out var handler))
            {
                await handler.Handle(args);
            }
            else
            {
                Console.WriteLine(GetHelpText());
                Environment.Exit(-1);
            }
        }

        protected abstract string GetHelpText();

        protected static string GetHelpText<T>() where T : struct
        {
            var cmds = EnumExtensions.GetDescriptions<T>();
            return "Supported commands are: " + string.Join(",", cmds);
        }
    }
}