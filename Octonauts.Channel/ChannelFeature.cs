using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    public class ChannelFeature : AbstractFeature
    {
        private enum Commands
        {
            [CommandDescription("help", "help")]
            HelpCmd,
            [CommandDescription("create", "Create a channel")]
            CreateCmd,
            [CommandDescription("delete", "Delete a channel")]
            DeleteCmd,
        }

        public override string GetHelpText()
        {
            return GetHelpText<Commands>();
        }

        [Option("command", "Specify the command to execute", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.CreateCmd.GetDescription().CommandName, new CreateChannelCmdHandler()
            },
            {
                Commands.DeleteCmd.GetDescription().CommandName, new DeleteChannelCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText())
            },
        };
    }
}