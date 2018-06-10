using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    internal class ChannelCommands : AbstractCommands
    {
        private enum Commands
        {
            [Description("help")] HelpCmd,
            [Description("create-channel")] CreateChannelCmd,
            [Description("delete-channel")] DeleteChannelCmd,
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
                Commands.CreateChannelCmd.GetDescription(), new CreateChannelCmdHandler()
            },
            {
                Commands.DeleteChannelCmd.GetDescription(), new DeleteChannelCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription(), new HelpCmdHandler(GetHelpText())
            },
        };
    }
}