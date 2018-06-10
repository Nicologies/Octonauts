using System.Threading.Tasks;
using Octonauts.Release.Commands;

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