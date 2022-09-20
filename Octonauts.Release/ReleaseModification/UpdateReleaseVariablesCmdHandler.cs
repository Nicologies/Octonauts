using System.Threading.Tasks;
using Octonauts.Core.CommandsFramework;
using Octonauts.Release.Params;

namespace Octonauts.Release.ReleaseModification
{
    internal class UpdateReleaseVariablesCmdHandler : CommandHandler<ModifyReleaseParams>
    {
        protected override async Task Execute(ModifyReleaseParams options)
        {
            await ModifyReleaseOperationExecutor.Execute(options, new UpdateReleaseVariablesOperation());
        }

        public override string FeatureName => ReleaseFeature.StaticFeatureName;
        public override string CommandName => "update-variables";
        public override string CommandDescription => "Update variable snapshot for a release for project(s) or project group";
    }
}