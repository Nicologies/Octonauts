using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Environment
{
    public class EnvironmentFeature : AbstractFeature
    {
        public static readonly string StaticFeatureName = "environment";
        public override string FeatureDescription => "This feature contains Environment related commands, for example: delete environments by regex pattern";

        public EnvironmentFeature(IEnumerable<ICommandHandler> handlers) : base(handlers, StaticFeatureName)
        {
        }
    }
}