using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class UpdateReleaseVariablesCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override async Task Execute(ReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new UpdateReleaseVariablesOperation());
        }
    }
}