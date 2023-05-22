using MongoDB.Driver;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace Csmmon.Data
{
    internal static class DataModelHelper<T>
        where T : DataModel, new()
    {
        public static readonly DataCollection<T> Collection = new(typeof(T).Name + "s");

        public static async ValueTask<bool> SaveAsync(T model, UpdateDefinition<T> updateDefinition, CancellationToken cancellationToken = default)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted or ModelState.Deserializing)
                return false;

            return await Collection.ModifyDocumentAsync(model, updateDefinition, cancellationToken);
        }

        public static async ValueTask<bool> DeleteAsync(T model, CancellationToken cancellationToken = default)
        {
            if (model.State is ModelState.Stateless or ModelState.Deleted)
                return false;

            model.State = ModelState.Deleted;

            return await Collection.DeleteDocumentAsync(model, cancellationToken);
        }

        public static async ValueTask<T?> GetAsync(Expression<Func<T, bool>> func, Action<T>? creationAction = null, CancellationToken cancellationToken = default)
        {
            var value = await Collection.FindDocumentAsync(func, cancellationToken);

            if (value is null)
            {
                if (creationAction is not null)
                    value = await CreateAsync(creationAction, cancellationToken);

                else
                    return default;
            }

            value.State = ModelState.Ready;

            return value;
        }

        public static async IAsyncEnumerable<T> GetManyAsync(Expression<Func<T, bool>> func, [EnumeratorCancellation] CancellationToken cancellationToken = default)
        {
            var value = Collection.FindManyDocumentsAsync(func, cancellationToken);

            await foreach(var v in value)
            {
                v.State = ModelState.Ready;
                yield return v;
            }
        }

        public static async ValueTask<T> CreateAsync(Action<T> action, CancellationToken cancellationToken = default)
        {
            var value = new T();

            action(value);

            await Collection.InsertOrUpdateDocumentAsync(value, cancellationToken);

            value.State = ModelState.Ready;

            return value;
        }
    }
}
