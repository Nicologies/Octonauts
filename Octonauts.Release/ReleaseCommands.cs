using coreArgs.Attributes;

namespace Octonauts.Release
{
    internal class ReleaseCommands
    {
        public const string CreateReleaseCmd = "create-release";
        public const string DeleteReleaseCmd = "delete-release";
        public const string UpdateReleaseVariablesCmd = "update-release-variables";
        public const string PromoteToChannelCmd = "promote-to-channel";

        [Option(CreateReleaseCmd, "Create octopus release", required: false)]
        public bool CreateRelease { get; set; }

        [Option(DeleteReleaseCmd, "Delete octopus release", required: false)]
        public bool DeleteRelease { get; set; }

        [Option(UpdateReleaseVariablesCmd, "Update variables for an octopus release", required: false)]
        public bool UpdateReleaseVariables { get; set; }

        [Option(PromoteToChannelCmd, "Indicates whether to promote a release to channel", required: false)]
        public bool PromoteToChannel { get; set; }
    }
}