using System;

namespace Daven.SyntaxExtensions
{
    public static class HandlerFacade
    {
        public static void Handle(this object source, params If[] items)
        {
            foreach (var item in items)
            {
                if (item.Call(source))
                {
                    return;
                }
            }
        }
    }

    public abstract class If
    {
        public static If<T2> ItemIs<T2>(Action<T2> action)
        {
            return new If<T2>()
            {
                action = action
            };
        }

        internal abstract bool Call(object source);
    }

    public class If<T> : If
    {
        internal Action<T> action;

        internal override bool Call(object source)
        {
            if (source is T)
            {
                action(source.Cast<T>());
                return true;
            }
            return false;
        }
    }
}