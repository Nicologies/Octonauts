using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;

namespace OctonautsCli.FeatureLevelCommands
{
    internal class AllFeatures
    {
        private readonly IDictionary<string, AbstractFeature> _allFeatures;

        public AllFeatures(IEnumerable<AbstractFeature> allFeatures)
        {
            _allFeatures = allFeatures.ToDictionary(x => x.FeatureName.ToLowerInvariant(), x => x);
        }

        public class FeatureOption
        {
            [Option('f', "feature", "Specify the feature to use, for example '--feature release' to use the release feature", required: true)]
            public string Feature { get; set; }
        }

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
            stringBuilder.AppendLine("\tOCTOPUS_CLI_SERVER for the server url");
            stringBuilder.AppendLine("\tOCTOPUS_CLI_API_KEY for the API key");
            stringBuilder.AppendLine("Supported Features:");
            foreach (var handler in _allFeatures)
            {
                stringBuilder.AppendLine($"\t{handler.Key} {handler.Value.FeatureDescription}");
                stringBuilder.AppendLine($"{handler.Value.GetHelpText("\t\t")}");
            }
            stringBuilder.AppendLine("\thelp\tshow above help information");


            return stringBuilder.ToString();
        }

        public async Task DispatchToFeature(string[] args)
        {
            if (!args.Any())
            {
                Console.WriteLine(GetHelpText());
                return;
            }

            var options = CommandArgsParser.Parse<FeatureOption>(args, "");
            if (_allFeatures.TryGetValue(options.Feature.ToLowerInvariant(), out var handler))
            {
                await handler.DispatchCommand(args);
            }
            else
            {
                Console.WriteLine(GetHelpText());
                Environment.Exit(-1);
            }
        }
    }
}
