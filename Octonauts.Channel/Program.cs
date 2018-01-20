using System;
using System.Threading.Tasks;
using coreArgs;
using Octonauts.Core;
using Octonauts.Core.OctopusClient;
using Octopus.Client.Exceptions;

namespace Octonauts.Channel
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            var options = ArgsParser.Parse<ChannelParams>(args);
            if (options.Errors.Count > 0)
            {
                Console.Write(ArgsParser.GetHelpText<ChannelParams>());
                Environment.Exit(-1);
                return;
            }
            var channelParams = options.Arguments;
            OctopusParamsBuilder.FillOctopusParams(channelParams);
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
