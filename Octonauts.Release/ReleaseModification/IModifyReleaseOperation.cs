using System.Threading.Tasks;
using Octonauts.Release.Params;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseModification
{
    internal interface IModifyReleaseOperation<in T> where T : ModifyReleaseParams
    {
        Task Execute(IOctopusAsyncClient client, T releaseParams, ReleaseResource release);
    }
}