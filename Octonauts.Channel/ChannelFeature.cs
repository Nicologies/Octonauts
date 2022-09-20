using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    public class ChannelFeature : AbstractFeature
    {
        public override string FeatureDescription => "This feature contains Environment related commands, for example: delete environments by regex pattern";

        public static readonly string StaticFeatureName = "channel";
        public ChannelFeature(IEnumerable<ICommandHandler> handlers) : base(handlers, StaticFeatureName)
        {
        }
    }
}