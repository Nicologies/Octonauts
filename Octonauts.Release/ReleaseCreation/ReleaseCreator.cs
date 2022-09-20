using System;
using System.Linq;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octonauts.Release.Params;
using Octopus.Client;
using Octopus.Client.Exceptions;
using Octopus.Client.Model;

namespace Octonauts.Release.ReleaseCreation
{
    internal static class ReleaseCreator
    {
        public static async Task CreateRelease(CreateReleaseParams releaseParams)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(releaseParams);
            var effectiveProjects = await releaseParams.GetEffectiveProjects(client);
            if (!effectiveProjects.Any())
            {
                await Console.Error.WriteLineAsync("No project specified, please use the --project-group or --projects");
                Environment.Exit(-1);
            }

            foreach (var project in effectiveProjects)
            {
                await CreateRelease(project, releaseParams, client);
            }
        }

        private static async Task CreateRelease(string projectName,
            CreateReleaseParams releaseParams, IOctopusAsyncClient octo)
        {
            var project = await octo.Repository.Projects.FindByName(projectName);
            var channel = await octo.GetChannelResource(project, releaseParams.Channel);
            if (channel == null)
            {
                Console.WriteLine($"Skipped as no such channel for {projectName}");
                return;
            }

            var releaseResource = new ReleaseResource()
            {
                ChannelId = channel.Id,
                ProjectId = project.Id,
                Version = releaseParams.GetEffectiveReleaseName()
            };
            var process = await octo.Repository.DeploymentProcesses.Get(project.DeploymentProcessId);
            var template = await octo.Repository.DeploymentProcesses.GetTemplate(process, channel);
            foreach (var package in template.Packages)
            {
                var selectedPackage = new SelectedPackage
                {
                    ActionName = package.ActionName,
                    Version = releaseParams.Version
                };
                releaseResource.SelectedPackages.Add(selectedPackage);
            }

            try
            {
                await octo.Repository.Releases.Create(releaseResource);
            }
            catch (OctopusValidationException octoEx)
            {
                if (octoEx.ToString()
                    .Contains("Please use a different version, or look at using a mask to auto-increment the number"))
                {
                    Console.WriteLine(
                        $"Skipped creating release as version {releaseParams.GetEffectiveReleaseName()} for {project.Name} already exists.");
                }
                else
                {
                    Console.Error.WriteLine(
                        $"Failed to create release for {projectName} in channel {channel.Name} {octoEx}");
                    throw;
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to create release for {projectName} in channel {channel.Name} {ex}");
                throw;
            }
        }

        public static async Task PromoteToChannel(CreateReleaseParams releaseParams)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(releaseParams);
            foreach (var projectStr in await releaseParams.GetEffectiveProjects(client))
            {
                var project = await client.Repository.Projects.FindByName(projectStr);
                if (project == null)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find this project");
                    continue;
                }

                var channel = await client.GetChannelResource(project, releaseParams.Channel);
                if (channel == null)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find the channel for this project");
                    continue;
                }

                try
                {
                    var release =
                        await client.Repository.Projects.GetReleaseByVersion(project,
                            releaseParams.GetEffectiveReleaseName());
                    if (release == null)
                    {
                        Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                        continue;
                    }

                    release.ChannelId = channel.Id;
                    await client.Repository.Releases.Modify(release);
                }
                catch (OctopusResourceNotFoundException)
                {
                    Console.WriteLine($"Skipped {projectStr} as cannot find the release for this project");
                }
            }
        }
    }
}
