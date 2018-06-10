using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.FeatureLevelCommands
{
    internal class FeatureCommands : AbstractCommands
    {
        private enum Features
        {
            [Description("release")]
            Release
        }

        [Option("feature", "The feature", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            { Features.Release.GetDescription(), new ReleaseFeatureCommandsHandler() },
        };

        protected override string GetHelpText()
        {
            return GetHelpText<Features>();
        }
    }
}
