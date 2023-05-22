using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Csmmon.Data
{
    public abstract class DataModel : IModel
    {
        [BsonId]
        public ObjectId ObjectId { get; set; }

        [BsonIgnore]
        public ModelState State { get; set; }

        public static async ValueTask<bool> SaveAsync<T, TField>(T? model, Expression<Func<T, TField>> expression, TField value, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => model is not null && await DataModelHelper<T>.SaveAsync(model, Builders<T>.Update.Set(expression, value), cancellationToken);

        public static async ValueTask<bool> DeleteAsync<T>(T model, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => await DataModelHelper<T>.DeleteAsync(model, cancellationToken);

        public static async ValueTask<T> CreateAsync<T>(Action<T> action, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => await DataModelHelper<T>.CreateAsync(action, cancellationToken);

        public static async ValueTask<T?> GetAsync<T>(Expression<Func<T, bool>> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => await DataModelHelper<T>.GetAsync(func, creationAction, cancellationToken);

        public static IAsyncEnumerable<T> GetManyAsync<T>(Expression<Func<T, bool>> func, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => DataModelHelper<T>.GetManyAsync(func, cancellationToken);
    }
}
