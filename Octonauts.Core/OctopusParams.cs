using coreArgs.Attributes;

namespace Nicologies.Octonauts.Core
{
    public class OctopusParams
    {
        [Option("api-key", "Octopus apikey", required: false)]
        public string ApiKey { get; set; }
        [Option("server-url", "Octopus service url", required: false)]
        public string ServerUrl { get; set; }
    }
}
