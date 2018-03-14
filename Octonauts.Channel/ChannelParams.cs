using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Channel
{
    public class ChannelParams : CommonParams
    {
        [Option("delete", "delete channel instead of create",
            required: false)]
        public bool DeleteChannel { get; set; } = false;

        [Option("lifecycle", "life cycle of the channel, default if not specified",
            required: false)]
        public string LifeCycle { get; set; }
    }
}
