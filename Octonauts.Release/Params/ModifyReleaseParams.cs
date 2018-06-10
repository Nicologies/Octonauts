using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Release.Params
{
    internal class ModifyReleaseParams : ProjectsParams
    {
        [Option("release-name", "Name of the release", required: true)]
        public string ReleaseName { get; set; }
    }
}
