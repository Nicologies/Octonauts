using System.Threading.Tasks;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class UpdateReleaseVariablesOperation : IReleaseOperation
    {
        public async Task Execute(IOctopusAsyncClient client, ReleaseParams releaseParams, ReleaseResource release)
        {
            await client.Repository.Releases.SnapshotVariables(release);
        }
    }
}