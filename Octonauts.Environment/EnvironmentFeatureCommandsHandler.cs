using Octonauts.Core.CommandsFramework;

namespace Octonauts.Environment
{
    public class EnvironmentFeatureCommandsHandler : FeatureHandler<EnvironmentFeature>
    {
        public EnvironmentFeatureCommandsHandler() : base(new EnvironmentFeature())
        {
        }
    }
}
