using MongoDB.Driver;
using System.Linq.Expressions;

namespace Csmmon.Data
{
    public static class DataModelExtensions
    {
        public static async ValueTask<bool> SaveAsync<T, TField>(this T? model, Expression<Func<T, TField>> expression, TField value, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => model is not null && await DataModelHelper<T>.SaveAsync(model, Builders<T>.Update.Set(expression, value), cancellationToken);

        public static async ValueTask<bool> DeleteAsync<T>(this T model, CancellationToken cancellationToken = default)
            where T : DataModel, new()
            => model is not null && await DataModelHelper<T>.DeleteAsync(model, cancellationToken);
    }
}
