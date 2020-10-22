using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using coreArgs.Attributes;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class AbstractFeature
    {
        [Option('c', "command", "The command to execute", required: true)]
        public string Command { get; set; }

        protected abstract Dictionary<string, ICommandHandler> Dispatcher { get; }

        public async Task DispatchCommand<T>(string[] args) where T : AbstractFeature
        {
            var options = CommandArgsParser.Parse<T>(args, GetHelpText(""));
            if (Dispatcher.TryGetValue(options.Command.ToLowerInvariant(), out var handler))
            {
                await handler.Handle(args);
            }
            else
            {
                Console.WriteLine(GetHelpText(""));
                Environment.Exit(-1);
            }
        }

        public abstract string GetHelpText(string indent);

        protected static string GetHelpText<T>(string indent) where T : struct
        {
            var cmds = CommandEnumExtensions.GetDescriptions<T>();
            var sb = new StringBuilder();
            sb.AppendLine($"{indent}Supported commands are: ");
            foreach (var cmd in cmds)
            {
                sb.AppendLine($"{indent}{cmd.CommandName.ToLowerInvariant()}:\t{cmd.Description}");
            }

            return sb.ToString();
        }
    }
}