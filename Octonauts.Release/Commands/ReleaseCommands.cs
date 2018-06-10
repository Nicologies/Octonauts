using System;
using System.ComponentModel;
using System.Linq;
using coreArgs.Attributes;

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

    public static class EnumExtensions
    {
        public static string GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return enumerationValue.ToString();
        }
    }
}