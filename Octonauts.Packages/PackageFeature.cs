using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Packages
{
    public class PackageFeature : AbstractFeature
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            HelpCmd,
            [CommandDescription("get-used", "Get packages used by project(s) or project group")]
            // ReSharper disable once InconsistentNaming
            GetPackagesCmd,
        }

        public override string GetHelpText(string indent)
        {
            return GetHelpText<Commands>(indent);
        }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new Dictionary<string, ICommandHandler>
        {
            {
                Commands.GetPackagesCmd.GetDescription().CommandName, new GetPackagesCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText(""))
            },
        };
    }
}