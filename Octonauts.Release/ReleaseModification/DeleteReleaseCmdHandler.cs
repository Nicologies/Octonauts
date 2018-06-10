using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseModification
{
    internal class DeleteReleaseCmdHandler : CommandHandler<ModifyReleaseParams>
    {
        protected override async Task Execute(ModifyReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new DeleteReleaseOperation());
        }
    }
}