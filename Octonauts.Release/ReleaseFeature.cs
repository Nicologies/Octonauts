using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Release
{
    public class ReleaseFeature : AbstractFeature
    {
        public static readonly string StaticFeatureName = "release";

        public override string FeatureDescription => "This feature contains Release related commands, such as create, delete, update variable snapshot and etc.";

        public ReleaseFeature(IEnumerable<ICommandHandler> handlers) : base(handlers, StaticFeatureName)
        {
        }
    }
}
