using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Release.ReleaseCmdHandlers;

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

        private static string GetHelpText()
        {
            var cmds = Enum.GetValues(typeof(Commands)).Cast<Commands>().Select(x => x.GetDescription());
            return "Supported commands are: " + string.Join(",", cmds);
        }

        public static async Task DispatchCommand(string[] args)
        {
            var options = CommandArgsParaser.Parse<ReleaseCommands>(args);
            if (Dispatcher.TryGetValue(options.Command.ToLowerInvariant(), out var handler))
            {
                await handler.Handle(args);
            }
            else
            {
                Console.WriteLine(GetHelpText());
                Environment.Exit(-1);
            }
        }

        private static readonly Dictionary<string, ICommandHandler> Dispatcher = new Dictionary<string, ICommandHandler>
        {
            {
                Commands.CreateReleaseCmd.GetDescription(), new CreateReleaseCmdHandler()
            },
            {
                Commands.PromoteToChannelCmd.GetDescription(), new PromoteToChannelCmdHandler()
            },
            {
                Commands.DeleteReleaseCmd.GetDescription(), new DeleteReleaseCmdHandler()
            },
            {
                Commands.UpdateReleaseVariablesCmd.GetDescription(),
                new UpdateReleaseVariablesCmdHandler()
            },
            {
                Commands.HelpCmd.GetDescription(), new HelpCmdHandler()
            },
        };
    }
}