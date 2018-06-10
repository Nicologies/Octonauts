using System.Threading.Tasks;
using Octonauts.Release.ReleaseCmdHandlers.Params;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal interface IModifyReleaseOperation<in T> where T : ModifyReleaseParams
    {
        Task Execute(IOctopusAsyncClient client, T releaseParams, ReleaseResource release);
    }
}