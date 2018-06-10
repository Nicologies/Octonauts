using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class CommandHandler<T> : ICommandHandler
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