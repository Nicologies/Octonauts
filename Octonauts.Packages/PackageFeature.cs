using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Packages
{
    public class PackageFeature : AbstractFeature
    {
        public static readonly string StaticFeatureName = "package";

        public override string FeatureDescription => "This feature contains Package related commands, for example: get packages used by project";

        public PackageFeature(IEnumerable<ICommandHandler> handlers) : base(handlers, StaticFeatureName)
        {
        }
    }
}