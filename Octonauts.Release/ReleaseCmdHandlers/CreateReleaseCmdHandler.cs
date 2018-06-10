using System.Threading.Tasks;
using Octonauts.Release.Commands;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class CreateReleaseCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override async Task Execute(ReleaseParams options)
        {
            await ReleaseCreator.CreateRelease(options);
        }
    }
}