using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseCreation
{
    internal class CreateReleaseCmdHandler : CommandHandler<CreateReleaseParams>
    {
        protected override async Task Execute(CreateReleaseParams options)
        {
            await ReleaseCreator.CreateRelease(options);
        }
    }
}