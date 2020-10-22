using Octonauts.Core.CommandsFramework;

namespace Octonauts.Release
{
    public class ReleaseFeatureCommandsHandler : FeatureHandler<ReleaseFeature>
    {
        public ReleaseFeatureCommandsHandler() : base(new ReleaseFeature())
        {
        }
    }
}