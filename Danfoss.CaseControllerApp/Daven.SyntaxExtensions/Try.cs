using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace Daven.SyntaxExtensions
{
    public static class Try
    {
        public static async Task<T> Async<T>(Func<Task<T>> @try, Func<Exception, Action, Task> @catch = null, Action @finally = null)
        {
            ExceptionDispatchInfo capturedException = null;
            try
            {
                return await @try();
            }
            catch (Exception ex)
            {
                capturedException = ExceptionDispatchInfo.Capture(ex);
            }
            finally
            {
                @finally?.Invoke();
            }

            if (capturedException != null)
            {
                if (@catch != null)
                {
                    await @catch(capturedException.SourceException, () => capturedException.Throw());
                }
                else
                {
                    capturedException.Throw();
                }    
            }

            return default (T);
        }
    }
}