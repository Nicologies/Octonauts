using System.Threading.Tasks;
using Octopus.Client;
using Octopus.Client.Model;

namespace Nicologies.Octonauts.Core.OctopusClient
{
    public static class OctopusChannelProvider
    {
        public static async Task<ChannelResource> GetChannelResource(this IOctopusAsyncClient octo, 
            ProjectResource project, string channelName)
        {
            var channel = await octo.Repository.Channels.FindByName(project, channelName);
            return channel;
        }
    }
}
