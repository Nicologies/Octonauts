using System;
using System.Threading.Tasks;
using coreArgs;
using Octonauts.Core;

namespace Octonauts.Release
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = ArgsParser.Parse<ReleaseParams>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<ReleaseParams>());
                Environment.Exit(-1);
                return;
            }

            DispatchWork(options.Arguments).Wait();
        }

        private static async Task DispatchWork(ReleaseParams options)
        {
            if (options.CreateRelease)
            {
                OctopusParamsBuilder.FillOctopusParams(options);
                await ReleaseCreator.CreateRelease(options);
                return;
            }
            if (options.PromoteToChannel)
            {
                OctopusParamsBuilder.FillOctopusParams(options);
                await ReleaseCreator.PromoteToChannel(options);
                return;
            }
            if (options.DeleteRelease)
            {
                OctopusParamsBuilder.FillOctopusParams(options);
                await ReleaseDeleter.DeleteRelease(options);
                return;
            }
        }
    }
}