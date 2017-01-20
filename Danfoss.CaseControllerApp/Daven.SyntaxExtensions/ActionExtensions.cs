using System;

namespace Daven.SyntaxExtensions
{
    public static class ActionExtensions
    {
        public static void Call(this Action action) => action?.Invoke();
        public static void Call<T>(this Action<T> action, T parameter) => action?.Invoke(parameter);
        public static void Call<T1, T2>(this Action<T1, T2> action, T1 p1, T2 p2) => action?.Invoke(p1, p2);

        public static T Call<T>(this Func<T> func)
        {
            if (func != null)
            {
                return func();
            }
            return default (T);
        }

        public static Func<T, bool> SelfOrTrue<T>(this Func<T, bool> filter) => filter ?? (x => true);
    }
}