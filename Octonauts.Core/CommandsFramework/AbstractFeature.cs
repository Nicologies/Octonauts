using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreArgs.Attributes;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class AbstractFeature
    {
        protected AbstractFeature(IEnumerable<ICommandHandler> handlers, string featureName)
        {
            Dispatcher = handlers.Where(x => x.FeatureName == featureName).ToDictionary(x => x.CommandName, x => x);
            FeatureName = featureName;
        }
        public class CommandOption
        {
            [Option('c', "command", "The command to execute", required: true)]
            public string Command { get; set; }
        }

        public string FeatureName { get; }
        public abstract string FeatureDescription { get; }

        protected IDictionary<string, ICommandHandler> Dispatcher { get; }

        public async Task DispatchCommand(string[] args)
        {
            var options = CommandArgsParser.Parse<CommandOption>(args, GetHelpText(""));
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

        public string GetHelpText(string indent)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"{indent}Use -command <command name>. Supported commands are: ");
            foreach (var handler in Dispatcher)
            {
                sb.AppendLine($"{indent}{handler.Value.CommandName.ToLowerInvariant()}:\t{handler.Value.CommandDescription}");
            }
            sb.AppendLine($"{indent}help:\tshow above help information");
            return sb.ToString();
        }
    }
}