using System.Collections.Generic;

namespace Daven.SyntaxExtensions
{
    public static class ListExtensions
    {
        public static void AddRange<T>(this List<T> list, params T[] args)
        {
            list.AddRange(args);
        }
    }
}