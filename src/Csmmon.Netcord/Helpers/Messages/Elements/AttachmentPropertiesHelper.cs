﻿using NetCord.Rest;

namespace Csmmon.Netcord
{
    public static class AttachmentPropertiesHelper
    {
        public static AttachmentProperties WithDescription(this AttachmentProperties properties, string description)
        {
            properties.Description = description;
            return properties;
        }

        public static AttachmentProperties WithFilename(this AttachmentProperties properties, string fileName)
        {
            properties.FileName = fileName;
            return properties;
        }
    }
}
