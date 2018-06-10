using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    public class MachineFeatureCommandsHandler : ICommandHandler
    {
        private readonly MachineCommands _commands = new MachineCommands();

        public async Task Handle(string[] args)
        {
            await _commands.DispatchCommand<MachineCommands>(args);
        }
    }
}
