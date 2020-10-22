using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Environment
{
    public class EnvironmentFeature : AbstractFeature
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            Help,
            [CommandDescription("delete", "Delete environments that matches regex pattern")]
            DeleteCmd,
        }

        public override string GetHelpText(string indent)
        {
            return GetHelpText<Commands>(indent);
        }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.DeleteCmd.GetDescription().CommandName, new DeleteEnvironmentsCmdHandler()
            },
            {
                Commands.Help.GetDescription().CommandName, new HelpCmdHandler(GetHelpText(""))
            },
        };
    }
}