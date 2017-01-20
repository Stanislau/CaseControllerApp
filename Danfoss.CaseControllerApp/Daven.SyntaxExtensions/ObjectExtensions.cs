using System;

namespace Daven.SyntaxExtensions
{
    public static class ObjectExtensions
    {
        public static T As<T>(this object self)
            where T: class
        {
            return self as T;
        }

        public static T Cast<T>(this object self)
        {
            return (T) self;
        }

        /// <summary>
        /// Synonum of Cast. Exist because of name conflicts with IEnumerable's method Cast.
        /// </summary>
        public static T CastInstanceTo<T>(this object self)
        {
            return self.Cast<T>();
        }

        public static void IfImplemented<T>(this object self, Action<T> action, Action othervise = null)
        {
            if (self is T)
            {
                action(self.Cast<T>());
            }
            else
            {
                othervise.Call();
            }
        }

        public static bool Isnt<T>(this object self)
        {
            return self is T == false;
        }

        public static T Do<T>(this T self, Action<T> action)
        {
            action(self);
            return self;
        }

        public static T Set<T>(this T self, Action<T> setter)
        {
            return Do(self, setter);
        }
    }
}