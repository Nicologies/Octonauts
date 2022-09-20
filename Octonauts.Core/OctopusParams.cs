using System;
using coreArgs.Attributes;

namespace Octonauts.Core
{
    public class OctopusParams
    {
        [Option("api-key", "Octopus apikey, can also be provided by environment variable: OCTOPUS_CLI_API_KEY", required: false)]
        public string ApiKey { get; set; }
        [Option("server-url", "Octopus service url, can also be provided by environment variable: OCTOPUS_CLI_SERVER", required: false)]
        public string ServerUrl { get; set; }

        public void FillParams()
        {
            FillOctopusParamsFromEnv();
            DoFillParams();
        }

        protected virtual void DoFillParams(){}

        private void FillOctopusParamsFromEnv()
        {
            var apikey = Environment.GetEnvironmentVariable("OCTOPUS_CLI_API_KEY");

            if (!string.IsNullOrWhiteSpace(apikey) && string.IsNullOrWhiteSpace(ApiKey))
            {
                ApiKey = apikey;
            }

            apikey = Environment.GetEnvironmentVariable("OCTOPUS_APIKEY");

            if (!string.IsNullOrWhiteSpace(apikey) && string.IsNullOrWhiteSpace(ApiKey))
            {
                ApiKey = apikey;
            }

            var server = Environment.GetEnvironmentVariable("OCTOPUS_CLI_SERVER");
            if (!string.IsNullOrWhiteSpace(server) && string.IsNullOrWhiteSpace(ServerUrl))
            {
                ServerUrl = server;
            }


            server = Environment.GetEnvironmentVariable("OCTOPUS_SERVERURL");
            if (!string.IsNullOrWhiteSpace(server) && string.IsNullOrWhiteSpace(ServerUrl))
            {
                ServerUrl = server;
            }
        }
    }
}
