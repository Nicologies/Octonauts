using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class CommandHandler<T> : ICommandHandler
        where T : OctopusParams
    {
        public async Task Handle(string[] args)
        {
            var options = CommandArgsParser.Parse<T>(args, "");
            options.FillParams();
            await Execute(options);
        }

        protected abstract Task Execute(T options);

        public abstract string FeatureName { get; }
        public abstract string CommandName { get; }
        public abstract string CommandDescription { get; }
    }
}