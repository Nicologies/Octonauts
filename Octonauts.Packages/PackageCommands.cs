using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Packages
{
    internal class PackageCommands : AbstractCommands
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            HelpCmd,
            [CommandDescription("get-used", "Get packages used by project(s) or project group")]
            GetPackagesCmd,
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
                Commands.GetPackagesCmd.GetDescription().CommandName, new GetPackagesCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText())
            },
        };
    }
}