using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Release
{
    public class ReleaseParams : CommonParams
    {
        [Option("release-name", "Name of the release otherwise the version number will be used", required: false)]
        public string ReleaseName { private get; set; }

        public string GetEffectiveReleaseName()
        {
            return string.IsNullOrWhiteSpace(ReleaseName) ? Version : ReleaseName.Trim();
        }
    }
}
