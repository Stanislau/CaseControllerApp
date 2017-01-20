using System;
using System.Threading;
using System.Threading.Tasks;

namespace Daven.SyntaxExtensions
{
    public static class SemaphoreSlimExtensions
    {
        public async static Task Lock(this SemaphoreSlim semaphore, Func<Task> action)
        {
            await semaphore.WaitAsync();
            try
            {
                await action();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public async static Task<T> Lock<T>(this SemaphoreSlim semaphore, Func<Task<T>> action)
        {
            await semaphore.WaitAsync();
            try
            {
                return await action();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}