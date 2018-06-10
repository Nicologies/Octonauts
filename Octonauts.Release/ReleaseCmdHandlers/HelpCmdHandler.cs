using System;
using System.Threading.Tasks;
using Octonauts.Release.CommandsFramework;

namespace Octonauts.Release.ReleaseCmdHandlers
{
    internal class HelpCmdHandler : CommandHandler<ReleaseParams>
    {
        private readonly string _helpText;

        public HelpCmdHandler(string helpText)
        {
            _helpText = helpText;
        }

        protected override Task Execute(ReleaseParams options)
        {
            Console.WriteLine(_helpText);
            return Task.FromResult(0);
        }
    }
}