using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseCreation
{
    internal class CreateReleaseCmdHandler : CommandHandler<CreateReleaseParams>
    {
        protected override async Task Execute(CreateReleaseParams options)
        {
            await ReleaseCreator.CreateRelease(options);
        }

        public override string FeatureName { get; } = ReleaseFeature.StaticFeatureName;
        public override string CommandName => "create";
        public override string CommandDescription => "Create a release for project(s) or project group";
    }
}