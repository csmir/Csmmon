﻿using NetCord.Rest;

namespace Csmmon.Netcord
{
    public static class EmbedFooterPropertiesHelper
    {
        public static EmbedFooterProperties WithText(this EmbedFooterProperties properties, string text)
        {
            properties.Text = text;
            return properties;
        }

        public static EmbedFooterProperties WithIconUrl(this EmbedFooterProperties properties, string imageUrl)
        {
            properties.IconUrl = imageUrl;
            return properties;
        }
    }
}
