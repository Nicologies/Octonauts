using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Channel;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Environment;
using Octonauts.Machines;
using Octonauts.Packages;
using Octonauts.Release;

namespace OctonautsCli.FeatureLevelCommands
{
    internal class AllFeatures
    {
        private class RootHelperCommandsHandler : IFeatureHandler
        {
            private readonly Func<string, string> _getHelpText;

            public RootHelperCommandsHandler(Func<string, string> getHelpText)
            {
                _getHelpText = getHelpText;
            }

            public async Task Handle(string[] args)
            {
                await Console.Out.WriteLineAsync(_getHelpText(""));
            }

            public string GetHelpText(string indent)
            {
                return $"{indent}show above help message";
            }
        }

        public AllFeatures()
        {
            Dispatcher.Add(Features.Help.GetDescription().CommandName, new RootHelperCommandsHandler(x => GetHelpText()));
        }

        private enum Features
        {
            [CommandDescription("help", "Help")]
            Help,
            [CommandDescription("release", "This feature contains Release related commands, such as create, delete, update variable snapshot and etc.")]
            Release,
            [CommandDescription("channel", "This feature contains Channel related commands, such as create and delete")]
            Channel,
            [CommandDescription("package", "This feature contains Package related commands, for example: get packages used by project")]
            Package,
            [CommandDescription("environment", "This feature contains Environment related commands, for example: delete environments by regex pattern")]
            Environment,
            [CommandDescription("machine", "This feature contains Machine related commands, for example: find machine by thumbprint")]
            Machine
        }

        [Option('f', "feature", "Specify the feature to use, for example '--feature release' to use the release feature", required: true)]
        public string Feature { get; set; }

        protected Dictionary<string, IFeatureHandler> Dispatcher = new Dictionary<string, IFeatureHandler>
        {
            { Features.Release.GetDescription().CommandName, new ReleaseFeatureCommandsHandler() },
            { Features.Channel.GetDescription().CommandName, new ChannelFeatureCommandsHandler() },
            { Features.Package.GetDescription().CommandName, new PackageFeatureCommandsHandler() },
            { Features.Environment.GetDescription().CommandName, new EnvironmentFeatureCommandsHandler() },
            { Features.Machine.GetDescription().CommandName, new MachineFeatureCommandsHandler() },
        };

        public string GetHelpText()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("This tool can be executed with arguments like:");
            stringBuilder.AppendLine("\t--feature <feature name> --command <command of feature> <arguments for the command>");
            stringBuilder.AppendLine("For example to find a machine by thumbprint:");
            stringBuilder.AppendLine("\t--feature machine --command find-by-thumbprint --thumbprint CAB8994D6B919C62E7B747FB");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("For each feature/command, you can use help to find out the required arguments such as:");
            stringBuilder.AppendLine("\t'--feature release help' to see the commands of the release feature");
            stringBuilder.AppendLine("\t'--feature release --command create help' to see the required args to create a release");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine("To avoid entering the Server Url and APIKey every time, you can define them as environment variables");
            stringBuilder.AppendLine("\tOCTOPUS_SERVERURL for the server url");
            stringBuilder.AppendLine("\tOCTOPUS_APIKEY for the API key");
            stringBuilder.AppendLine("Supported Features:");
            foreach (var handler in Dispatcher)
            {
                stringBuilder.AppendLine($"\t{handler.Key}");
                stringBuilder.AppendLine($"{handler.Value.GetHelpText("\t\t")}");
            }

            return stringBuilder.ToString();
        }

        public async Task DispatchToFeature(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine(GetHelpText());
                return;
            }

            var options = CommandArgsParser.Parse<AllFeatures>(args, "");
            if (Dispatcher.TryGetValue(options.Feature.ToLowerInvariant(), out var handler))
            {
                await handler.Handle(args);
            }
            else
            {
                Console.WriteLine(GetHelpText());
                Environment.Exit(-1);
            }
        }
    }
}
