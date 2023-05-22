using NetCord.Rest;

namespace Csmmon.Netcord
{
    public static class MessageReferencePropertiesHelper
    {
        public static MessageReferenceProperties WithMessageId(this MessageReferenceProperties properties, ulong messageId)
        {
            properties.Id = messageId;
            return properties;
        }

        public static MessageReferenceProperties WithFailIfNotExists(this MessageReferenceProperties properties, bool failIfNotExists)
        {
            properties.FailIfNotExists = failIfNotExists;
            return properties;
        }
    }
}
