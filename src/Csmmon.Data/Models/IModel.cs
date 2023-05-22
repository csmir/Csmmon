using System.Linq.Expressions;

namespace Csmmon.Data
{
    public interface IModel
    {
        public static async ValueTask<T> CreateAsync<T>(Action<T> action)
            where T : DataModel, new()
            => await DataModel.CreateAsync(action);

        public static async ValueTask<T?> GetAsync<T>(Expression<Func<T, bool>> func, Action<T>? creationAction = null)
            where T : DataModel, new()
            => await DataModel.GetAsync(func, creationAction);

        public static IAsyncEnumerable<T> GetManyAsync<T>(Expression<Func<T, bool>> func)
            where T : DataModel, new()
            => DataModel.GetManyAsync(func);
    }
}
