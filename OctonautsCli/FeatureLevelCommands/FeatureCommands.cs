using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Channel;
using Octonauts.Core.CommandsFramework;
using Octonauts.Environment;
using Octonauts.Machines;
using Octonauts.Packages;
using Octonauts.Release;

namespace OctonautsCli.FeatureLevelCommands
{
    internal class FeatureCommands : AbstractCommands
    {
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
            Machine,
        }

        [Option("feature", "Specify the feature to use, or '--feature help' to see available features", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            { Features.Release.GetDescription().CommandName, new ReleaseFeatureCommandsHandler() },
            { Features.Channel.GetDescription().CommandName, new ChannelFeatureCommandsHandler() },
            { Features.Package.GetDescription().CommandName, new PackageFeatureCommandsHandler() },
            { Features.Environment.GetDescription().CommandName, new EnvironmentFeatureCommandsHandler() },
            { Features.Machine.GetDescription().CommandName, new MachineFeatureCommandsHandler() },
            { Features.Help.GetDescription().CommandName, new HelpCmdHandler(GetHelpText()) },
        };

        protected override string GetHelpText()
        {
            return GetHelpText<Features>();
        }
    }
}
