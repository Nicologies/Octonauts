using System.Threading.Tasks;
using Octopus.Client;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal interface IReleaseOperation
    {
        Task Execute(IOctopusAsyncClient client, ReleaseParams releaseParams, ReleaseResource release);
    }
}