using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public interface IFeatureHandler : ICommandHandler
    {
        string GetHelpText(string indent);
    }

    public abstract class FeatureHandler<T> : IFeatureHandler where T : AbstractFeature
    {
        private readonly T _feature;

        protected FeatureHandler(T feature)
        {
            _feature = feature;
        }

        public string GetHelpText(string indent)
        {
            return _feature.GetHelpText(indent);
        }

        public async Task Handle(string[] args)
        {
            await _feature.DispatchCommand<T>(args);
        }
    }
}
