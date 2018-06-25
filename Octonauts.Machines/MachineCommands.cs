using System.Collections.Generic;
using coreArgs.Attributes;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    internal class MachineCommands : AbstractCommands
    {
        private enum Commands
        {
            [CommandDescription("help", "Help")]
            HelpCmd,
            [CommandDescription("deploy-project", "Individually deploy a project to machines in an environment")]
            DeployProject,
            [CommandDescription("find-by-thumbprint", "Find a machine by its thumbprint")]
            FindByThumbprint,
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
                Commands.DeployProject.GetDescription().CommandName, new DeployProjectToMachinesCmdHandler()
            },
            {
                Commands.FindByThumbprint.GetDescription().CommandName, new FindByThumbprintCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription().CommandName, new HelpCmdHandler(GetHelpText())
            },
        };
    }
}