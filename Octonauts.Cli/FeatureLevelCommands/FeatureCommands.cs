using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Channel;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
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
            Channel
        }

        [Option("feature", "The feature", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            { Features.Release.GetDescription(), new ReleaseFeatureCommandsHandler() },
            { Features.Channel.GetDescription(), new ChannelFeatureCommandsHandler() },
        };

        protected override string GetHelpText()
        {
            return GetHelpText<Features>();
        }
    }
}
