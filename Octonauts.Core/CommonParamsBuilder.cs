using System.IO;
using YamlDotNet.Serialization;

namespace Nicologies.Octonauts.Core
{
    public static class CommonParamsBuilder
    {
        public static T GetCommonParams<T>(BaseOptions options) where T : CommonParams, new()
        {
            var commonParams = GetParamsFromFile<T>(options);

            OctopusParamsBuilder.FillOctopusParams(commonParams, options);

            MergeParamsFromCommandlineArgs(options, commonParams);

            return commonParams;
        }

        private static T GetParamsFromFile<T>(BaseOptions options) where T : CommonParams, new()
        {
            try
            {
                var des = new DeserializerBuilder().Build();
                using (var fileStream = File.OpenRead(options.File))
                {
                    using (var stream = new StreamReader(fileStream))
                    {
                        return des.Deserialize<T>(stream);
                    }
                }
            }
            catch
            {
                return new T();
            }
        }

        private static void MergeParamsFromCommandlineArgs<T>(BaseOptions options, T releaseCreationParams) where T : CommonParams
        {
            if (!string.IsNullOrWhiteSpace(options.Version))
            {
                releaseCreationParams.Version = options.Version.Trim();
            }

            if (!string.IsNullOrWhiteSpace(options.Channel))
            {
                releaseCreationParams.Channel = options.Channel;
            }
        }
    }
}
