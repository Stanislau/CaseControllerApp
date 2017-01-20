using System;
using Danfoss.CaseControllerApp.CrossCutting.Exceptions;

namespace Danfoss.CaseControllerApp.Apple.Exceptions
{
    public class UserInterfaceException : DanfossException
    {
        public UserInterfaceException()
        {
        }

        public UserInterfaceException(string message) : base(message)
        {
        }

        public UserInterfaceException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}