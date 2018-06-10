using coreArgs.Attributes;

namespace Octonauts.Channel
{
    public class CreateChannelParams : ChannelParams
    {
        [Option("lifecycle", "life cycle of the channel, default if not specified",
            required: false)]
        public string LifeCycle { get; set; }
    }
}
