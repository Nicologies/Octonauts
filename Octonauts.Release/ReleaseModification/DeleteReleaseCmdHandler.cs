using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseModification
{
    internal class DeleteReleaseCmdHandler : CommandHandler<ModifyReleaseParams>
    {
        protected override async Task Execute(ModifyReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new DeleteReleaseOperation());
        }

        public override string FeatureName { get; } = ReleaseFeature.StaticFeatureName;
        public override string CommandName => "delete";
        public override string CommandDescription => "Delete a release from project(s) or project group";
    }
}