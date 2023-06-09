﻿using NetCord.Rest;

namespace Csmmon.Netcord
{
    public static class EmbedAuthorPropertiesHelper
    {
        public static EmbedAuthorProperties WithAuthor(this EmbedAuthorProperties properties, GuildUser author)
        {
            properties.Name = author.Nickname ?? author.Username;
            properties.IconUrl = author.HasGuildAvatar ? author.GetGuildAvatarUrl().ToString() : author.GetAvatarUrl().ToString();
            return properties;
        }

        public static EmbedAuthorProperties WithAuthor(this EmbedAuthorProperties properties, User author)
        {
            properties.Name = author.Username;
            properties.IconUrl = author.GetAvatarUrl().ToString();
            return properties;
        }

        public static EmbedAuthorProperties WithName(this EmbedAuthorProperties properties, string name)
        {
            properties.Name = name;
            return properties;
        }

        public static EmbedAuthorProperties WithUrl(this EmbedAuthorProperties properties, string url)
        {
            properties.Url = url;
            return properties;
        }

        public static EmbedAuthorProperties WithIconUrl(this EmbedAuthorProperties properties, string iconUrl)
        {
            properties.IconUrl = iconUrl;
            return properties;
        }
    }
}
