using System;
using System.ComponentModel;
using System.Linq;
using coreArgs.Attributes;
using Octonauts.Core;

namespace Octonauts.Release.Commands
{
    internal class ReleaseCommands
    {
        public enum Commands
        {
            [Description("help")] HelpCmd,
            [Description("create-release")] CreateReleaseCmd,
            [Description("delete-release")] DeleteReleaseCmd,
            [Description("update-release-variables")]
            UpdateReleaseVariablesCmd,
            [Description("promote-to-channel")] PromoteToChannelCmd,
        }

        [Option("command", "command to execute", required: true)]
        public string Command { get; set; }

        public static string GetHelpText()
        {
            var cmds = Enum.GetValues(typeof(Commands)).Cast<Commands>().Select(x => x.GetDescription());
            return "Supported commands are: " + string.Join(",", cmds);
        }
    }
}