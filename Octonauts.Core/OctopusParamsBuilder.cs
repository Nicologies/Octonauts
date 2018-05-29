using System;

namespace Octonauts.Core
{
    public static class OctopusParamsBuilder
    {
        public static void FillOctopusParams(this OctopusParams octopusParams)
        {
            FillOctopusParamsFromEnv(octopusParams);
        }

        private static void FillOctopusParamsFromEnv(this OctopusParams octopusParams)
        {
            var apikey = Environment.GetEnvironmentVariable("OCTOPUS_APIKEY");
            if (!string.IsNullOrWhiteSpace(apikey) && string.IsNullOrWhiteSpace(octopusParams.ApiKey))
            {
                octopusParams.ApiKey = apikey;
            }

            var server = Environment.GetEnvironmentVariable("OCTOPUS_SERVERURL");
            if (!string.IsNullOrWhiteSpace(server) && string.IsNullOrWhiteSpace(octopusParams.ServerUrl))
            {
                octopusParams.ServerUrl = server;
            }
        }
    }
}