using MongoDB.Bson;
using MongoDB.Driver;

namespace Csmmon.Data
{
    public sealed class StorageProvider
    {
        private static MongoClient? _client;
        private static MongoDatabaseBase? _database;

        public StorageConfiguration Configuration { get; }

        public static bool IsConnected { get; private set; } = false;

        public StorageProvider(StorageConfiguration config)
        {
            if (config.DatabaseUri is not null)
            {
                if (string.IsNullOrEmpty(config.DatabaseName))
                    throw new ArgumentNullException(nameof(config), "The database name set in configuration was found null.");

                _client = new MongoClient(config.DatabaseUri);

                if (_client is not null)
                {
                    _database = _client.GetDatabase(config.DatabaseName) as MongoDatabaseBase;

                    if (!TryConnection())
                        throw new InvalidOperationException("Databases could not connect.");

                    IsConnected = true;
                }
                else
                    throw new InvalidOperationException("Client cannot resolve and was found null.");
            }

            Configuration = config;
        }

        public static MongoCollectionBase<T> GetMongoCollection<T>(string name)
            where T : DataModel, new()
        {
            if (WaitConnection())
            {
                var collection = (_database?.GetCollection<T>(name) as MongoCollectionBase<T>)!;
                return collection;
            }
            return null!;
        }

        public static bool TryConnection()
        {
            try
            {
                _client?.ListDatabaseNames();
                return true;
            }
            catch (MongoException)
            {
                return false;
            }
        }

        public static bool WaitConnection()
        {
            while (!IsConnected)
            {
                Task.Delay(5);
            }

            return true;
        }

        public static string RunCommand(string command)
        {
            try
            {
                var result = _database?.RunCommand<BsonDocument>(BsonDocument.Parse(command));
                return result.ToJson();
            }
            catch (Exception ex) when (ex is FormatException or MongoCommandException)
            {
                throw;
            }
        }
    }
}
