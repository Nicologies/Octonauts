using System.Threading.Tasks;

namespace Octonauts.Release.Commands
{
    internal interface ICommandHandler
    {
        Task Handle(string[] args);
    }
}