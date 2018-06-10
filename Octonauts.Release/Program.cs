using System.Threading.Tasks;
using Octonauts.Release.Commands;

namespace Octonauts.Release
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await ReleaseCommands.DispatchCommand(args);
        }
    }
}