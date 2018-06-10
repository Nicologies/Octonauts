using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public interface ICommandHandler
    {
        Task Handle(string[] args);
    }
}