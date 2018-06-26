using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.ReleaseCreation;
using Octonauts.Release.ReleaseModification;

namespace Octonauts.Release
{
    internal class ReleaseCommands : AbstractCommands
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            HelpCmd,
            [CommandDescription("create", "Create a release for project(s) or project group")]
            CreateReleaseCmd,
            [CommandDescription("delete", "Delete a release from project(s) or project group")]
            DeleteReleaseCmd,
            [CommandDescription("delete-by-range", "Batch delete releases by version range")]
            DeleteReleasesByRangeCmd,
            [CommandDescription("update-variables", "Update variable snapshot for a release for project(s) or project group")]
            UpdateReleaseVariablesCmd,
            [CommandDescription("promote-to-channel", "Promote a release to another channel for project(s) or project group")]
            PromoteToChannelCmd,
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
                Commands.CreateReleaseCmd.GetDescription().CommandName, new CreateReleaseCmdHandler()
            },
            {
                Commands.PromoteToChannelCmd.GetDescription().CommandName, new PromoteToChannelCmdHandler()
            },
            {
                Commands.DeleteReleaseCmd.GetDescription().CommandName, new DeleteReleaseCmdHandler()
            },
            {
                Commands.DeleteReleasesByRangeCmd.GetDescription().CommandName, new DeleteReleasesByRangeCmdHandler()
            },
            {
                Commands.UpdateReleaseVariablesCmd.GetDescription().CommandName, new UpdateReleaseVariablesCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText())
            },
        };
    }
}
