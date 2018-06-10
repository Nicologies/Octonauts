using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;
using Octonauts.Release.ReleaseCmdHandlers.Params;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class DeleteReleaseCmdHandler : CommandHandler<ModifyReleaseParams>
    {
        protected override async Task Execute(ModifyReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new DeleteReleaseOperation());
        }
    }
}