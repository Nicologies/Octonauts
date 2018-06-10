using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Release.ReleaseCmdHandlers.Params
{
    internal class CreateReleaseParams : ProjectsParams
    {
        [Option("channel", "channel", required: true)]
        public string Channel { get; set; }

        [Option("version", "version", required: true)]
        public string Version { get; set; }

        [Option("release-name", "Name of the release otherwise the version number will be used", required: false)]
        public string ReleaseName { private get; set; }

        public string GetEffectiveReleaseName()
        {
            return string.IsNullOrWhiteSpace(ReleaseName) ? Version : ReleaseName.Trim();
        }
    }
}
