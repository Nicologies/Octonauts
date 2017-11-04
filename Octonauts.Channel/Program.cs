using System;
using System.Threading.Tasks;
using coreArgs;
using coreArgs.Attributes;
using Nicologies.Octonauts.Core;
using Nicologies.Octonauts.Core.OctopusClient;
using Octopus.Client.Exceptions;

namespace Nicologies.Octonauts.Channel
{
    internal class Options: BaseOptions
    {
        [Option("project-group", "create channel for all projects in this project group",
            required: false)]
        public string ProjectGroup { get; set; }

        [Option("delete", "delete channel instead of create",
            required: false)]
        public bool DeleteChannel { get; set; } = false;

        [Option("lifecycle", "life cycle of the channel, default if not specified",
            required: false)]
        public string LifeCycle { get; set; }
    }

    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var options = ArgsParser.Parse<Options>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<Options>());
                Environment.Exit(-1);
                return;
            }

            var channelParams = CommonParamsBuilder.GetCommonParams<ChannelParams>(options.Arguments);
            if (!string.IsNullOrWhiteSpace(options.Arguments.ProjectGroup))
            {
                channelParams.ProjectGroup = options.Arguments.ProjectGroup.Trim();
            }
            channelParams.DeleteChannel = options.Arguments.DeleteChannel;
            channelParams.LifeCycle = options.Arguments.LifeCycle;
            await CreateChannel(channelParams);
        }

        private static async Task CreateChannel(ChannelParams channelParams)
        {
            using (var client = await OctopusClientProvider.GetOctopusClient(channelParams))
            {
                foreach (var projectStr in channelParams.GetEffectiveProjects(client))
                {
                    var project = await client.Repository.Projects.FindByName(projectStr);
                    var channel = await client.GetChannelResource(project, channelParams.Channel);
                    if (!channelParams.DeleteChannel)
                    {
                        if (channel != null)
                        {
                            Console.WriteLine($"Skipped create channel {channelParams.Channel} as it already exists in project {project.Name}");
                            continue;
                        }
                        Console.WriteLine($"Creating channel {channelParams.Channel} in project {project.Name}");
                        var channelEditor = await client.Repository.Channels.CreateOrModify(project, channelParams.Channel);
                        if (!string.IsNullOrWhiteSpace(channelParams.LifeCycle))
                        {
                            var lifeCycle =
                                await client.Repository.Lifecycles.FindByName(channelParams.LifeCycle.Trim());
                            channelEditor.UsingLifecycle(lifeCycle);
                            await channelEditor.Save();
                        }
                    }
                    else
                    {
                        if (channel == null)
                        {
                            Console.WriteLine($"Skipped deleting channel {channelParams.Channel} as it doesn't exist in project {project.Name}");
                            continue;
                        }
                        Console.WriteLine($"Deleting channel {channelParams.Channel} from project {project.Name}");
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
    }
}
