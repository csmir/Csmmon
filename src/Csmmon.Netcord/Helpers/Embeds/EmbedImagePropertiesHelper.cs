using NetCord.Rest;

namespace Csmmon.Netcord
{
    public static class EmbedImagePropertiesHelper
    {
        public static EmbedImageProperties WithUrl(this EmbedImageProperties properties, string url)
        {
            properties.Url = url;
            return properties;
        }
    }
}
