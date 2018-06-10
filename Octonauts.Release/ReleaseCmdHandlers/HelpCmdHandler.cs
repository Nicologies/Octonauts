using System;
using System.Threading.Tasks;
using Octonauts.Release.Commands;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class HelpCmdHandler : CommandHandler<ReleaseParams>
    {
        protected override Task Execute(ReleaseParams options)
        {
            Console.WriteLine(ReleaseCommands.GetHelpText());
            return Task.FromResult(0);
        }
    }
}