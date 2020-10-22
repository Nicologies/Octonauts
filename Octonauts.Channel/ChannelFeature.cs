using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    public class ChannelFeature : AbstractFeature
    {
        private enum Commands
        {
            [CommandDescription("help", "shows the available commands")]
            HelpCmd,
            [CommandDescription("create", "Create a channel")]
            CreateCmd,
            [CommandDescription("delete", "Delete a channel")]
            DeleteCmd,
        }

        public override string GetHelpText(string indent)
        {
            return GetHelpText<Commands>(indent);
        }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.CreateCmd.GetDescription().CommandName, new CreateChannelCmdHandler()
            },
            {
                Commands.DeleteCmd.GetDescription().CommandName, new DeleteChannelCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText(""))
            },
        };
    }
}