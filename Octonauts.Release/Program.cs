using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Release.Commands;
using Octonauts.Release.ReleaseCmdHandlers;

namespace Octonauts.Release
{
    internal class Program
    {
        private static readonly Dictionary<string, ICommandHandler> Dispatcher = new Dictionary<string, ICommandHandler>
        {
            {
                ReleaseCommands.Commands.CreateReleaseCmd.GetDescription(), new CreateReleaseCmdHandler()
            },
            {
                ReleaseCommands.Commands.PromoteToChannelCmd.GetDescription(), new PromoteToChannelCmdHandler()
            },
            {
                ReleaseCommands.Commands.DeleteReleaseCmd.GetDescription(), new DeleteReleaseCmdHandler()
            },
            {
                ReleaseCommands.Commands.UpdateReleaseVariablesCmd.GetDescription(),
                new UpdateReleaseVariablesCmdHandler()
            },
            {
                ReleaseCommands.Commands.HelpCmd.GetDescription(), new HelpCmdHandler()
            },
        };

        public static async Task Main(string[] args)
        {
            var options = CommandArgsParaser.Parse<ReleaseCommands>(args);
            if (Dispatcher.TryGetValue(options.Command.ToLowerInvariant(), out var handler))
            {
                await handler.Handle(args);
            }
            else
            {
                Console.WriteLine(ReleaseCommands.GetHelpText());
                Environment.Exit(-1);
            }
        }
    }
}