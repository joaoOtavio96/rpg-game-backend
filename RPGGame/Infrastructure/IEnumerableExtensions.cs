﻿namespace RPGGame.Infrastructure
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>>GetPermutations<T>(this IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
