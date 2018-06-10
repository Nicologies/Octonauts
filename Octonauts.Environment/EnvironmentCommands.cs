using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Environment
{
    internal class EnvironmentCommands : AbstractCommands
    {
        private enum Commands
        {
            [Description("help")] HelpCmd,
            [Description("delete")] DeleteChannelCmd,
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
                Commands.DeleteChannelCmd.GetDescription(), new DeleteEnvironmentsCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription(), new HelpCmdHandler(GetHelpText())
            },
        };
    }
}