using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csmmon
{
    public static class RangeExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items,
            CancellationToken cancellationToken = default)
        {
            var results = new List<T>();
            await foreach (var item in items.WithCancellation(cancellationToken).ConfigureAwait(false))
                results.Add(item);
            return results;
        }

        public static IEnumerable<T> CastWhere<T>(this IEnumerable input)
        {
            foreach (var @in in input)
                if (@in is T @out)
                    yield return @out;
        }

        public static T? SelectFirstOrDefault<T>(this IEnumerable input, T? defaultValue = default)
        {
            foreach (var @in in input)
                if (@in is T @out)
                    return @out;

            return defaultValue;
        }

        public static T[,] Extract<T>(this T[,] array, int yDimension)
        {
            T[,] result = new T[array.GetLength(0), array.GetLength(1) - 1];

            for (int i = 0, j = 0; i < array.GetLength(0); i++)
            {
                for (int k = 0, u = 0; k < array.GetLength(1); k++)
                {
                    if (k == yDimension)
                        continue;

                    result[j, u] = array[i, k];
                    u++;
                }
                j++;
            }

            return result;
        }
    }
}
