using System;

namespace Nicologies.Octonauts.Core
{
    public static class OctopusParamsBuilder
    {
        public static void FillOctopusParams(OctopusParams octopusParams, BaseOptions cmdArgs)
        {
            FillOctopusParamsFromCommandLineArgs(octopusParams, cmdArgs);
            FillOctopusParamsFromEnv(octopusParams);
        }

        private static void FillOctopusParamsFromCommandLineArgs(OctopusParams octopusParams, BaseOptions cmdArgs)
        {
            if (!string.IsNullOrWhiteSpace(cmdArgs.ApiKey))
            {
                octopusParams.ApiKey = cmdArgs.ApiKey;
            }
            if (!string.IsNullOrWhiteSpace(cmdArgs.ServerUrl))
            {
                octopusParams.ServerUrl = cmdArgs.ServerUrl;
            }
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