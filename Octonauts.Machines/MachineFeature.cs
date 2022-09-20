using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    public class MachineFeature : AbstractFeature
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            HelpCmd,
            [CommandDescription("deploy-project", "Individually deploy a project to machines in an environment")]
            DeployProject,
            [CommandDescription("find-by-thumbprint", "Find a machine by its thumbprint")]
            FindByThumbprint,
            [CommandDescription("list-machines", "list machines in an environment")]
            ListMachines,
            [CommandDescription("set-roles", "set roles of the machine")]
            SetRoles,
        }

        public override string GetHelpText(string indent)
        {
            return GetHelpText<Commands>(indent);
        }

        protected override Dictionary<string, ICommandHandler> Dispatcher => new()
        {
            {
                Commands.DeployProject.GetDescription().CommandName, new DeployProjectToMachinesCmdHandler()
            },
            {
                Commands.FindByThumbprint.GetDescription().CommandName, new FindByThumbprintCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText(""))
            },
            {
                Commands.ListMachines.GetDescription().CommandName, new ListMachinesCmdHandler()
            },
            {
                Commands.SetRoles.GetDescription().CommandName, new SetRolesCmdHandler()
            },
        };
    }
}