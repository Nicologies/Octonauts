using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public interface ICommandHandler
    {
        string FeatureName{ get; }
        string CommandName{ get; }
        string CommandDescription{ get; }
        Task Handle(string[] args);
    }
}