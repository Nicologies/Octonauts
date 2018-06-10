using System;
using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public class HelpCmdHandler : CommandHandler<OctopusParams>
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