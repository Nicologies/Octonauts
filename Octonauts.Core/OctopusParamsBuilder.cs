using System;

namespace Nicologies.Octonauts.Core
{
    public static class OctopusParamsBuilder
    {
        public static void FillOctopusParams(OctopusParams octopusParams)
        {
            FillOctopusParamsFromEnv(octopusParams);
        }

        private static void FillOctopusParamsFromEnv(OctopusParams octopusParams)
        {
            var apikey = Environment.GetEnvironmentVariable("OCTOPUS_APIKEY");
            if (!string.IsNullOrWhiteSpace(apikey))
            {
                octopusParams.ApiKey = apikey;
            }

            var server = Environment.GetEnvironmentVariable("OCTOPUS_SERVERURL");
            if (!string.IsNullOrWhiteSpace(server))
            {
                octopusParams.ServerUrl = server;
            }
        }
    }
}