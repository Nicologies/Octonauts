using Octonauts.Core.CommandsFramework;

namespace Octonauts.Packages
{
    public class PackageFeatureCommandsHandler : FeatureHandler<PackageFeature>
    {
        public PackageFeatureCommandsHandler() : base(new PackageFeature())
        {
        }
    }
}