using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Packages
{
    public class PackageFeatureCommandsHandler : ICommandHandler
    {
        private readonly PackageCommands _commands = new PackageCommands();

        public async Task Handle(string[] args)
        {
            await _commands.DispatchCommand<PackageCommands>(args);
        }
    }
}