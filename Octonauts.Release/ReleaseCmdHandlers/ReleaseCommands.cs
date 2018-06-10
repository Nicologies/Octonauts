using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class ReleaseCommands : AbstractCommands
    {
        private enum Commands
        {
            [Description("help")] HelpCmd,
            [Description("create-release")] CreateReleaseCmd,
            [Description("delete-release")] DeleteReleaseCmd,

            [Description("update-release-variables")]
            UpdateReleaseVariablesCmd,
            [Description("promote-to-channel")] PromoteToChannelCmd,
        }

        protected override string GetHelpText()
        {
            return GetHelpText<Commands>();
        }

        [Option("command", "The command to execute", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.CreateReleaseCmd.GetDescription(), new CreateReleaseCmdHandler()
            },
            {
                Commands.PromoteToChannelCmd.GetDescription(), new PromoteToChannelCmdHandler()
            },
            {
                Commands.DeleteReleaseCmd.GetDescription(), new DeleteReleaseCmdHandler()
            },
            {
                Commands.UpdateReleaseVariablesCmd.GetDescription(),
                new UpdateReleaseVariablesCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription(), new HelpCmdHandler(GetHelpText())
            },
        };
    }
}
