using System;
using System.Threading.Tasks;
using Octonauts.Core;
using Octonauts.Core.CommandsFramework;
using Octonauts.Core.OctopusClient;

namespace Octonauts.Channel
{
    internal class CreateChannelCmdHandler : CommandHandler<CreateChannelParams>
    {
        protected override async Task Execute(CreateChannelParams options)
        {
            using var client = await OctopusClientProvider.GetOctopusClient(options);
            foreach (var projectStr in await options.GetEffectiveProjects(client))
            {
                var project = await client.Repository.Projects.FindByName(projectStr);
                var channel = await client.GetChannelResource(project, options.Channel);

                if (channel != null)
                {
                    Console.WriteLine(
                        $"Skipped create channel {options.Channel} as it already exists in project {project.Name}");
                    continue;
                }

                Console.WriteLine($"Creating channel {options.Channel} in project {project.Name}");
                var channelEditor = await client.Repository.Channels.CreateOrModify(project, options.Channel);
                if (!string.IsNullOrWhiteSpace(options.LifeCycle))
                {
                    var lifeCycle =
                        await client.Repository.Lifecycles.FindByName(options.LifeCycle.Trim());
                    channelEditor.UsingLifecycle(lifeCycle);
                    await channelEditor.Save();
                }
            }
        }
    }
}