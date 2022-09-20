using System;
using coreArgs.Attributes;

namespace Octonauts.Core
{
    public class OctopusParams
    {
        [Option("api-key", "Octopus apikey", required: false)]
        public string ApiKey { get; set; }
        [Option("server-url", "Octopus service url", required: false)]
        public string ServerUrl { get; set; }

        public void FillParams()
        {
            FillOctopusParamsFromEnv();
            DoFillParams();
        }

        protected virtual void DoFillParams(){}

        private void FillOctopusParamsFromEnv()
        {
            var apikey = Environment.GetEnvironmentVariable("OCTOPUS_APIKEY");
            if (!string.IsNullOrWhiteSpace(apikey) && string.IsNullOrWhiteSpace(ApiKey))
            {
                ApiKey = apikey;
            }

            var server = Environment.GetEnvironmentVariable("OCTOPUS_SERVERURL");
            if (!string.IsNullOrWhiteSpace(server) && string.IsNullOrWhiteSpace(ServerUrl))
            {
                ServerUrl = server;
            }
        }
    }
}
