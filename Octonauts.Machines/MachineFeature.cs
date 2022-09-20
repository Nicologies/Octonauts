using System.Collections.Generic;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    public class MachineFeature : AbstractFeature
    {
        public static readonly string StaticFeatureName = "machine";
        
        public override string FeatureDescription => "This feature contains Machine related commands, for example: find machine by thumbprint";

        public MachineFeature(IEnumerable<ICommandHandler> handlers) : base(handlers, StaticFeatureName)
        {
        }
    }
}