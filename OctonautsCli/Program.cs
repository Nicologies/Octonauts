using System.Threading.Tasks;
using OctonautsCli.FeatureLevelCommands;

namespace OctonautsCli
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            await new FeatureFeature().DispatchCommand<FeatureFeature>(args);
        }
    }
}
