using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Channel
{
    public class ChannelParams : ProjectsParams
    {
        [Option("channel", "channel", required: true)]
        public string Channel { get; set; }
    }
}