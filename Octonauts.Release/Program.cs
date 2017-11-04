using System;
using System.Threading.Tasks;
using coreArgs;
using Nicologies.Octonauts.Core;

namespace Nicologies.Octonauts.Release
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = ArgsParser.Parse<Options>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<Options>());
                Environment.Exit(-1);
                return;
            }

            DispatchWork(options.Arguments).Wait();
        }

        private static async Task DispatchWork(Options options)
        {
            if (options.CreateRelease)
            {
                var releaseCreationParams = CommonParamsBuilder.GetCommonParams<ReleaseCreationParams>(options);
                releaseCreationParams.ReleaseName = options.ReleaseName;
                await ReleaseCreator.CreateRelease(releaseCreationParams);
                return;
            }
            if (options.PromoteToChannel)
            {
                var releaseCreationParams = CommonParamsBuilder.GetCommonParams<ReleaseCreationParams>(options);
                releaseCreationParams.ReleaseName = options.ReleaseName;
                await ReleaseCreator.PromoteToChannel(releaseCreationParams);
                return;
            }
        }
    }
}