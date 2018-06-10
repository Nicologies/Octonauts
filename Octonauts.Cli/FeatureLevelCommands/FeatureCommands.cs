using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Channel;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Environment;
using Octonauts.Machines;
using Octonauts.Packages;
using Octonauts.Release;

namespace Octonauts.Cli.FeatureLevelCommands
{
    internal class FeatureCommands : AbstractCommands
    {
        private enum Features
        {
            [Description("release")]
            Release,
            [Description("channel")]
            Channel,
            [Description("package")]
            Package,
            [Description("environment")]
            Environment,
            [Description("machine")]
            Machine,
        }

        [Option("feature", "The feature", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            { Features.Release.GetDescription(), new ReleaseFeatureCommandsHandler() },
            { Features.Channel.GetDescription(), new ChannelFeatureCommandsHandler() },
            { Features.Package.GetDescription(), new PackageFeatureCommandsHandler() },
            { Features.Environment.GetDescription(), new EnvironmentFeatureCommandsHandler() },
            { Features.Machine.GetDescription(), new MachineFeatureCommandsHandler() },
        };

        protected override string GetHelpText()
        {
            return GetHelpText<Features>();
        }
    }
}
