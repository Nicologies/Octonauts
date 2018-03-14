using System.Threading.Tasks;
using Octopus.Client;

namespace Octonauts.Core.OctopusClient
{
    public static class OctopusClientProvider
    {
        public static Task<IOctopusAsyncClient> GetOctopusClient(OctopusParams releaseCreationParams)
        {
            var endpoint = GetOctopusServerEndpoint(releaseCreationParams);
            return OctopusAsyncClient.Create(endpoint);
        }

        private static OctopusServerEndpoint GetOctopusServerEndpoint(OctopusParams releaseCreationParams)
        {
            var endpoint = new OctopusServerEndpoint(releaseCreationParams.ServerUrl, releaseCreationParams.ApiKey);
            return endpoint;
        }
    }
}
