using MongoDB.Driver;
using System.Text.Json;

namespace Csmmon.Data
{
    /// <summary>
    ///     Represents the configuration for a mongo database.
    /// </summary>
    public class StorageConfiguration
    {
        /// <summary>
        ///     The database to use.
        /// </summary>
        public string DatabaseName { get; set; } = string.Empty;

        /// <summary>
        ///     The url to connect to.
        /// </summary>
        public MongoUrl? DatabaseUri { get; set; }
    }
}
