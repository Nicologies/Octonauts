using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Environment
{
    public class EnvironmentFeatureCommandsHandler : ICommandHandler
    {
        private readonly EnvironmentCommands _commands = new EnvironmentCommands();

        public async Task Handle(string[] args)
        {
            await _commands.DispatchCommand<EnvironmentCommands>(args);
        }
    }
}
