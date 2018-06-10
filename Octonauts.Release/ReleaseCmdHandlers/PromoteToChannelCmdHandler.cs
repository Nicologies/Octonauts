using System.Threading.Tasks;
using Octonauts.Release.Commands;

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