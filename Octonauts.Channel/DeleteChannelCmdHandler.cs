using System;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Exceptions;

namespace Octonauts.Channel
{
    internal class DeleteChannelCmdHandler : CommandHandler<ChannelParams>
    {
        protected override async Task Execute(ChannelParams options)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            foreach (var projectStr in await options.GetEffectiveProjects(client))
            {
                var project = await client.Repository.Projects.FindByName(projectStr);
                var channel = await client.GetChannelResource(project, options.Channel);

                if (channel == null)
                {
                    Console.WriteLine(
                        $"Skipped deleting channel {options.Channel} as it doesn't exist in project {project.Name}");
                    continue;
                }

                Console.WriteLine($"Deleting channel {options.Channel} from project {project.Name}");
                try
                {
                    await client.Repository.Channels.Delete(channel);
                }
                catch (OctopusValidationException ex)
                {
                    if (ex.Message.Contains("This channel has releases including"))
                    {
                        Console.WriteLine($"Unable to delete channel for {project.Name}: {ex.Message}");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    }
}