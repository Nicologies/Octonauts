using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;
using Octonauts.Release.ReleaseCmdHandlers.Params;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class PromoteToChannelCmdHandler : CommandHandler<CreateReleaseParams>
    {
        protected override async Task Execute(CreateReleaseParams options)
        {
            await ReleaseCreator.PromoteToChannel(options);
        }
    }
}