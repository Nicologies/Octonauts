using System.Threading.Tasks;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal static class ReleaseVariableUpdater
    {
        public static async Task Update(ReleaseParams options)
        {
            await ReleaseOperationExecutor.Execute(options, new UpdateReleaseVariablesOperation());
        }
    }
}
