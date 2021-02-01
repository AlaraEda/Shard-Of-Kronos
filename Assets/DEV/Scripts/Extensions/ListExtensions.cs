using System.Collections.Generic;

namespace DEV.Scripts.Extensions
{
    public static class ListExtensions
    {
        public static void AddNonExists<T>(this IList<T> list, T element)
        {
            if (!list.Contains(element)) list.Add(element);
        }
    }
}