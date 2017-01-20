using System;
using System.Threading.Tasks;

namespace Daven.SyntaxExtensions
{
    public static class TaskHelper
    {
        public static Task Complete()
        {
            var result = new TaskCompletionSource<bool>();
            result.SetResult(true);
            return result.Task;
        }

        public static Task<T> Complete<T>(T result)
        {
            var r = new TaskCompletionSource<T>();
            r.SetResult(result);
            return r.Task;
        }

        public static void RunSync(Func<Task> asyncMethod)
        {
            Task.Run(asyncMethod).Wait();
        }

        public static Task EmptyAction()
        {
            return Complete();
        }
    }
}