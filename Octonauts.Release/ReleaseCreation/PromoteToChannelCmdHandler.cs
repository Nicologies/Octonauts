using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseCreation
{
    internal class PromoteToChannelCmdHandler : CommandHandler<CreateReleaseParams>
    {
        protected override async Task Execute(CreateReleaseParams options)
        {
            await ReleaseCreator.PromoteToChannel(options);
        }

        public override string FeatureName => ReleaseFeature.StaticFeatureName;
        public override string CommandName => "promote-to-channel";
        public override string CommandDescription => "Promote a release to another channel for project(s) or project group";
    }
}