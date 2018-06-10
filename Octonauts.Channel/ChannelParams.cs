using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Channel
{
    public class ChannelParams : ProjectsParams
    {
        [Option("delete", "delete channel instead of create",
            required: false)]
        public bool DeleteChannel { get; set; } = false;

        [Option("channel", "channel", required: true)]
        public string Channel { get; set; }

        [Option("lifecycle", "life cycle of the channel, default if not specified",
            required: false)]
        public string LifeCycle { get; set; }
    }
}
