using System;

namespace Danfoss.CaseControllerApp.CrossCutting.Exceptions
{
    public class DanfossException : Exception
    {
        public DanfossException()
        {
        }

        public DanfossException(string message) : base(message)
        {
        }

        public DanfossException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}