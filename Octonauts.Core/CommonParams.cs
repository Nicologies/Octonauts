
using coreArgs.Attributes;

namespace Nicologies.Octonauts.Core
{
    public class CommonParams : ProjectsParams
    {
        [Option("channel", "channel", required: false)]
        public string Channel { get; set; }
        [Option("version", "version", required: false)]
        public string Version { get; set; }
    }
}
