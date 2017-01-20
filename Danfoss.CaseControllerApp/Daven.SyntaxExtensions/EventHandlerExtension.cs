using System;
using System.Threading;
using System.Threading.Tasks;

namespace Daven.SyntaxExtensions
{
    public delegate Task EventHandlerAsync(object sender, EventArgs e);

    public delegate Task EventHandlerAsync<in TEventArgs>(object sender, TEventArgs e);

    public static class EventHandlerExtensions
    {
        public static Task Raise<T>(this EventHandlerAsync<T> handler, object sender, T args)
        {
            var temp = Volatile.Read(ref handler);
            if (temp != null)
            {
                return temp.Invoke(sender, args);
            }
            return TaskHelper.Complete();
        }

        public static void Raise<T>(this EventHandler<T> handler, object sender, T args)
        {
            var temp = Volatile.Read(ref handler);
            temp?.Invoke(sender, args);
        }

        public static Task Raise(this EventHandlerAsync handler, object sender)
        {
            var temp = Volatile.Read(ref handler);
            if (temp != null)
            {
                return temp.Invoke(sender, EventArgs.Empty);
            }
            return TaskHelper.Complete();
        }

        public static void Raise(this EventHandler handler, object sender)
        {
            var temp = Volatile.Read(ref handler);
            temp?.Invoke(sender, EventArgs.Empty);
        }
    }
}