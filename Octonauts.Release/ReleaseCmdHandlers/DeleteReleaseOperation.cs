using System.Threading.Tasks;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class DeleteReleaseOperation : IReleaseOperation
    {
        public async Task Execute(IOctopusAsyncClient client, ReleaseParams releaseParams, ReleaseResource release)
        {
            await client.Repository.Releases.Delete(release);
        }
    }
}