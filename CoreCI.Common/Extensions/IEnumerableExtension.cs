using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCI.Common.Extensions
{
    /// <summary>
    /// Extensions for IEnumerable
    /// </summary>
    public static class IEnumerableExtension
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> target, Action<T> action)
        {
            foreach (T item in target)
            {
                action(item);
            }
            return target;
        }
    }
}
