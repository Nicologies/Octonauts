using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Machines
{
    internal class FindByThumbprintParams : OctopusParams
    {
        [Option("thumbprint", "Machine thumbprint")]
        public string Thumbprint { get; set; }
    }
}
