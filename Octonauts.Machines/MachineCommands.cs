using System.Collections.Generic;
using System.ComponentModel;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    internal class MachineCommands : AbstractCommands
    {
        private enum Commands
        {
            [Description("help")] HelpCmd,
            [Description("deploy-project")] DeployProject,
            [Description("find-by-thumbprint")] FindByThumbprint,
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
                Commands.DeployProject.GetDescription(), new DeployProjectToMachinesCmdHandler()
            },
            {
                Commands.FindByThumbprint.GetDescription(), new FindByThumbprintCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription(), new HelpCmdHandler(GetHelpText())
            },
        };
    }
}