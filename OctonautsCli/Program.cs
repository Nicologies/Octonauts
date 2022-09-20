using System.Threading.Tasks;
using Autofac;
using OctonautsCli.FeatureLevelCommands;

namespace OctonautsCli
{
    internal static class Program
    {
        public static async Task Main(string[] args)
        {
            var container = IocSetup.Setup();
            var allFeatures = container.Resolve<AllFeatures>();
            await allFeatures.DispatchToFeature(args);
        }
    }
}
