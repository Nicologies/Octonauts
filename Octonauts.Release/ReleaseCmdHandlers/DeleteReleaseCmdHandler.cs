using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class DeleteReleaseCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override async Task Execute(ReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new DeleteReleaseOperation());
        }
    }
}