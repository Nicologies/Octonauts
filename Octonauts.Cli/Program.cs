using System.Threading.Tasks;
using Octonauts.Cli.FeatureLevelCommands;

namespace Octonauts.Cli
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await new FeatureCommands().DispatchCommand<FeatureCommands>(args);
        }
    }
}
