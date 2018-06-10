using System.Threading.Tasks;
using Octonauts.Core;

namespace Octonauts.Release
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var options = CommandArgsParaser.Parse<ReleaseCommands>(args);

            await DispatchWork(options, args);
        }

        private static async Task DispatchWork(ReleaseCommands options, string[] commandlineArgs)
        {
            var releaseParams = CommandArgsParaser.Parse<ReleaseParams>(commandlineArgs);
            releaseParams.FillOctopusParams();
            if (options.CreateRelease)
            {
                await ReleaseCreator.CreateRelease(releaseParams);
                return;
            }

            if (options.PromoteToChannel)
            {
                await ReleaseCreator.PromoteToChannel(releaseParams);
                return;
            }

            if (options.DeleteRelease)
            {
                await ReleaseOperationExecutor.Execute(releaseParams, new DeleteReleaseOperation());
                return;
            }

            if (options.UpdateReleaseVariables)
            {
                await ReleaseOperationExecutor.Execute(releaseParams, new UpdateReleaseVariablesOperation());
                return;
            }
        }
    }
}