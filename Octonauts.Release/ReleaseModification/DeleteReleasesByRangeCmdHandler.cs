using System;
using System.Linq;
using System.Threading.Tasks;
using coreArgs.Attributes;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Exceptions;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseModification
{
    internal class DeleteReleasesByRangeParams : ProjectsParams
    {
        [Option("from-version", "Delete releases greater or equal to this version, SemVer is supported")]
        public string FromVersion { get; set; }

        [Option("to-version", "Delete releases less or equal to this version, SemVer is supported")]
        public string ToVersion { get; set; }
    }

    internal class DeleteReleasesByRangeCmdHandler : CommandHandler<DeleteReleasesByRangeParams>
    {
        protected override async Task Execute(DeleteReleasesByRangeParams options)
        {
            if (!SemanticVersion.TryParse(options.FromVersion, out var from))
            {
                Console.Error.WriteLine($"Invalid 'from' version: {options.FromVersion}");
                return;
            }

            if (!SemanticVersion.TryParse(options.ToVersion, out var to))
            {
                Console.Error.WriteLine($"Invalid 'to' version: {options.ToVersion}");
                return;
            }

            using var client = await OctopusClientProvider.GetOctopusClient(options);
            foreach (var projectStr in await options.GetEffectiveProjects(client))
            {
                var project = await client.Repository.Projects.FindByName(projectStr);
                if (project == null)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find this project");
                    continue;
                }

                var releases =
                    await client.Repository.Projects.GetAllReleases(project);
                if (releases == null || !releases.Any())
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                    continue;
                }

                foreach (var release in releases)
                {
                    if (!SemanticVersion.TryParse(release.Version, out var current))
                    {
                        Console.WriteLine(
                            $"Skipped deleting a version for project {projectStr} as unable to interpret version: {release.Version}");
                        continue;
                    }

                    if (current < from || current > to)
                    {
                        continue;
                    }

                    Console.WriteLine($"Deleting {release.Version} for project {projectStr}");
                    try
                    {
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
