using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Machines
{
    internal class DeployParams : OctopusParams
    {
        [Option("project", "project to deploy", required: true)]
        public string Project { get; set; }

        [Option("version", "version to deploy", required: true)]
        public string Version { get; set; }

        [Option("env", "environment", required: true)]
        public string Environment { get; set; }

        [Option("sleep", "sleep couple of seconds after performed action for a machine", required: false)]
        public short SleepSeconds { get; set; } = 0;
    }
}
