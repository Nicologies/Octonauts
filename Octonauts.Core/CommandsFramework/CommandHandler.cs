using System;
using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class CommandHandler<T> : ICommandHandler
        where T : OctopusParams
    {
        public async Task Handle(string[] args)
        {
            var options = CommandArgsParser.Parse<T>(args, "");
            options.FillOctopusParams();
            if (string.IsNullOrWhiteSpace(options.ApiKey))
            {
                await Console.Error.WriteLineAsync("Octopus Api Key is required, you can provide it via command line: --api-key API-XXXXX or environment variable: OCTOPUS_APIKEY");
                return;
            }

            if (string.IsNullOrWhiteSpace(options.ServerUrl))
            {
                await Console.Error.WriteLineAsync("Octopus server URL is required, you can provide it via command line: --server-url http://xxx.com or environment variable: OCTOPUS_SERVERURL");
                return;
            }

            await Execute(options);
        }

        protected abstract Task Execute(T options);
    }
}