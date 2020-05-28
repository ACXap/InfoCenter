using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Expansion
{
    public static class ExpansionMethod
    {
        public static IEnumerable<IEnumerable<T>> SplitCollection<T>(this IEnumerable<T> source, int chunksize)
        {
            if (chunksize <= 0) throw new ArgumentException("Размер части должен быть больше 0", "chunksize");

            while (source.Any())
            {
                yield return source.Take(chunksize);
                source = source.Skip(chunksize);
            }
        }
    }
}