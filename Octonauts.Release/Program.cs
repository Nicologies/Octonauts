using System.Threading.Tasks;
using Octonauts.Release.FeatureLevelCommands;

namespace Octonauts.Release
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            await new FeatureCommands().DispatchCommand<FeatureCommands>(args);
        }
    }
}