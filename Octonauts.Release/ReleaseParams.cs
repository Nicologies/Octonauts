using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Release
{
    public class ReleaseParams : CommonParams
    {
        [Option("release-name", "Name of the release otherwise the version number will be used", required: false)]
        public string ReleaseName { private get; set; }

        [Option("create-release", "Create octopus release", required: false)]
        public bool CreateRelease { get; set; }

        [Option("delete-release", "Delete octopus release", required: false)]
        public bool DeleteRelease { get; set; }

        [Option("promote-to-channel", "Indicates whether to promote a release to channel", required: false)]
        public bool PromoteToChannel { get; set; }

        public string GetEffectiveReleaseName()
        {
            return string.IsNullOrWhiteSpace(ReleaseName) ? Version : ReleaseName.Trim();
        }
    }
}
