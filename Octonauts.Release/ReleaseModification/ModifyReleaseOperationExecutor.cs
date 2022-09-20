using System;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octonauts.Release.Params;
using Octopus.Client.Exceptions;

namespace Octonauts.Release.ReleaseModification
{
    internal static class ModifyReleaseOperationExecutor
    {
        public static async Task Execute<T>(T options, IModifyReleaseOperation<T> operation) where T : ModifyReleaseParams
        {
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            foreach (var projectStr in await options.GetEffectiveProjects(client))
            {
                var project = await client.Repository.Projects.FindByName(projectStr);
                if (project == null)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find this project");
                    continue;
                }

                try
                {
                    var release =
                        await client.Repository.Projects.GetReleaseByVersion(project,
                            options.ReleaseName);
                    if (release == null)
                    {
                        Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                        continue;
                    }

                    await operation.Execute(client, options, release);
                }
                catch (OctopusResourceNotFoundException)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                }
            }
        }
    }
}
