using System.Threading.Tasks;
using Octonauts.Release.Commands;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class UpdateReleaseVariablesCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override async Task Execute(ReleaseParams options)
        {
            await ReleaseOperationExecutor.Execute(options, new UpdateReleaseVariablesOperation());
        }
    }
}