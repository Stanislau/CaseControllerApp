using System.Collections.Generic;

namespace Daven.SyntaxExtensions
{
    public static class DictionaryExtensions
    {
        public static T2 GetValueOrDefault<T1, T2>(this IDictionary<T1, T2> dict, T1 key)
            where T2 : class
        {
            T2 result;
            return dict.TryGetValue(key, out result)  ? result : null;
        }
    }
}