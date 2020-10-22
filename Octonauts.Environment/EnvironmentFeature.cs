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

        public override string GetHelpText()
        {
            return GetHelpText<Commands>();
        }

        [Option("command", "The command to execute", required: true)]
        public override string Command { get; set; }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.DeleteCmd.GetDescription().CommandName, new DeleteEnvironmentsCmdHandler()
            },
            {
                Commands.Help.GetDescription().CommandName, new HelpCmdHandler(GetHelpText())
            },
        };
    }
}