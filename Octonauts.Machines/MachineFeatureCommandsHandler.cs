using Octonauts.Core.CommandsFramework;

namespace Octonauts.Machines
{
    public class MachineFeatureCommandsHandler : FeatureHandler<MachineFeature>
    {
        public MachineFeatureCommandsHandler() : base(new MachineFeature())
        {
        }
    }
}
