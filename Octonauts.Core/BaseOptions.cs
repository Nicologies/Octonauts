using coreArgs.Attributes;

namespace Nicologies.Octonauts.Core
{
    public class BaseOptions
    {
        [Option("apikey", "octopus apikey", required: false)]
        public string ApiKey { get; set; }

        [Option("serverUrl", "Octopus service url", required: false)]
        public string ServerUrl { get; set; }

        [Option("file", "Configuration file path", required: false)]
        public string File { get; set; }

        [Option("channel", "channel", required: false)]
        public string Channel { get; set; }

        [Option("version", "version", required: false)]
        public string Version { get; set; }
    }
}
