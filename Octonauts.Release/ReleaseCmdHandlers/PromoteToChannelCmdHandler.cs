using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class PromoteToChannelCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override async Task Execute(ReleaseParams options)
        {
            await ReleaseCreator.PromoteToChannel(options);
        }
    }
}