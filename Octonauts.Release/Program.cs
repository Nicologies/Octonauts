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
            options.FillOctopusParams();
            if (options.CreateRelease)
            {
                await ReleaseCreator.CreateRelease(options);
                return;
            }

            if (options.PromoteToChannel)
            {
                await ReleaseCreator.PromoteToChannel(options);
                return;
            }

            if (options.DeleteRelease)
            {
                await ReleaseOperationExecutor.Execute(options, new DeleteReleaseOperation());
                return;
            }

            if (options.UpdateReleaseVariables)
            {
                await ReleaseOperationExecutor.Execute(options, new UpdateReleaseVariablesOperation());
                return;
            }
        }
    }
}