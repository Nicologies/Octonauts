using System.Threading.Tasks;

namespace Octonauts.Release.CommandsFramework
{
    internal interface ICommandHandler
    {
        Task Handle(string[] args);
    }
}