using System.Threading.Tasks;
using Octonauts.Release.Params;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseModification
{
    internal class DeleteReleaseOperation : IModifyReleaseOperation<ModifyReleaseParams>
    {
        public async Task Execute(IOctopusAsyncClient client, ModifyReleaseParams releaseParams, ReleaseResource release)
        {
            await client.Repository.Releases.Delete(release);
        }
    }
}