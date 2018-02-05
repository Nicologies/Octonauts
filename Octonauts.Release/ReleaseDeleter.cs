using System;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Exceptions;

namespace Octonauts.Release
{
    internal class ReleaseDeleter
    {
        public static async Task DeleteRelease(ReleaseParams options)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(options))
            {
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
                                options.GetEffectiveReleaseName());
                        if (release == null)
                        {
                            Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                            continue;
                        }
                        await client.Repository.Releases.Delete(release);
                    }
                    catch (OctopusResourceNotFoundException)
                    {
                        Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                    }
                }
            }
        }
    }
}