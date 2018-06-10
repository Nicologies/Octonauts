using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;

namespace Octonauts.Channel
{
    public class ChannelFeatureCommandsHandler : ICommandHandler
    {
        private readonly ChannelCommands _commands = new ChannelCommands();

        public async Task Handle(string[] args)
        {
            await _commands.DispatchCommand<ChannelCommands>(args);
        }
    }
}
