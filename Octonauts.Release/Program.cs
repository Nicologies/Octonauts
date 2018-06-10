using System.Threading.Tasks;
using Octonauts.Core;

namespace Octonauts.Release
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var options = CommandArgsParaser.Parse<ReleaseParams>(args);

            DispatchWork(options).Wait();
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