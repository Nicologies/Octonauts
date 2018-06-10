using System;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Release
{
    internal class HelpCmdHandler : CommandHandler<OctopusParams>
    {
        private readonly string _helpText;

        public HelpCmdHandler(string helpText)
        {
            _helpText = helpText;
        }

        protected override Task Execute(OctopusParams options)
        {
            Console.WriteLine(_helpText);
            return Task.FromResult(0);
        }
    }
}