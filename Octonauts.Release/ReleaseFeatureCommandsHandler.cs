using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Release
{
    public class ReleaseFeatureCommandsHandler : ICommandHandler
    {
        private readonly ReleaseCommands _releaseCommands = new ReleaseCommands();

        public async Task Handle(string[] args)
        {
            await _releaseCommands.DispatchCommand<ReleaseCommands>(args);
        }
    }
}