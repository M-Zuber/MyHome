using System;
using System.Collections.Generic;

namespace MyHome.Infrastructure
{
    public static class Extensions
    {
        public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            foreach (var element in source)
            {
                target.Add(element);
            }
        }
    }
}
