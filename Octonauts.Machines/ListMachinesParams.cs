using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Machines
{
    internal class ListMachinesParams : OctopusParams
    {
        [Option("environment", "filter by environment", required: false)]
        public string Environment { get; set; }
    }
}