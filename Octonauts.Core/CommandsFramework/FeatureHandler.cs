using System.Threading.Tasks;

namespace Octonauts.Core.CommandsFramework
{
    public abstract class FeatureHandler<T> : ICommandHandler where T : AbstractFeature
    {
        private readonly T _feature;

        protected FeatureHandler(T feature)
        {
            _feature = feature;
        }

        public string GetHelpText()
        {
            return _feature.GetHelpText();
        }

        public async Task Handle(string[] args)
        {
            await _feature.DispatchCommand<T>(args);
        }
    }
}
