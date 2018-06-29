using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Environment
{
    public class EnvironmentDeletionParams : OctopusParams
    {
        [Option('e', "environment-name-regex", "", required: true)]
        public string EnvironmentNameRegex { get; set; }

        [Option("dry-run", "list the environments without actually deleting them", required: false)]
        public bool DryRun { get; set; } = false;
    }
}