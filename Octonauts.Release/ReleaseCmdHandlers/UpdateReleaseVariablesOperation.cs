using System.Threading.Tasks;
using Octonauts.Release.ReleaseCmdHandlers.Params;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class UpdateReleaseVariablesOperation : IModifyReleaseOperation<ModifyReleaseParams>
    {
        public async Task Execute(IOctopusAsyncClient client, ModifyReleaseParams releaseParams, ReleaseResource release)
        {
            await client.Repository.Releases.SnapshotVariables(release);
        }
    }
}