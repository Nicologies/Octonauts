using System.Threading.Tasks;
using Octonauts.Cli.FeatureLevelCommands;

namespace Octonauts.Cli
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            await new FeatureCommands().DispatchCommand<FeatureCommands>(args);
        }
    }
}
