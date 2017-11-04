using Nicologies.Octonauts.Core;

namespace Nicologies.Octonauts.Release
{
    public class ReleaseCreationParams : CommonParams
    {
        public string ReleaseName { private get; set; }

        public string GetEffectiveReleaseName()
        {
            return string.IsNullOrWhiteSpace(ReleaseName) ? Version : ReleaseName.Trim();
        }
    }
}
