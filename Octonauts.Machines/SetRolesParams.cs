using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Machines
{
    internal class SetRolesParams : OctopusParams
    {
        [Option("roles", "roles to set", required: true)]
        public System.Collections.Generic.List<string> Roles { get; set; }

        [Option("machine-id", "machine id", required: true)]
        public string MachineId { get; set; }
    }
}