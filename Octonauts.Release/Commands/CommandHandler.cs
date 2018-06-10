using System.Threading.Tasks;
using Octonauts.Core;

namespace Octonauts.Release.Commands
{
    internal abstract class CommandHandler<T> : ICommandHandler
        where T : OctopusParams
    {
        public async Task Handle(string[] args)
        {
            var options = CommandArgsParaser.Parse<T>(args);
            options.FillOctopusParams();
            await Execute(options);
        }

        protected abstract Task Execute(T options);
    }
}