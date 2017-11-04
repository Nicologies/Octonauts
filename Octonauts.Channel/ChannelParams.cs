using Nicologies.Octonauts.Core;

namespace Nicologies.Octonauts.Channel
{
    public class ChannelParams: CommonParams
    {
        public bool DeleteChannel { get; set; } = false;
        public string LifeCycle { get; set; }
    }
}
