using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    public class ChannelFeatureCommandsHandler : FeatureHandler<ChannelFeature>
    {
        public ChannelFeatureCommandsHandler() : base(new ChannelFeature())
        {
        }
    }
}
