using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;
using Octonauts.Release.ReleaseCmdHandlers;

namespace Octonauts.Release.FeatureLevelCommands
{
    internal class ReleaseFeatureCommandsHandler : ICommandHandler
    {
        private readonly ReleaseCommands _releaseCommands = new ReleaseCommands();

        public async Task Handle(string[] args)
        {
            await _releaseCommands.DispatchCommand<ReleaseCommands>(args);
        }
    }
}