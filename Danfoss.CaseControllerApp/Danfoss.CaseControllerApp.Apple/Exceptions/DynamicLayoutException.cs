using System;

namespace Danfoss.CaseControllerApp.Apple.Exceptions
{
    public class DynamicLayoutException : UserInterfaceException
    {
        public DynamicLayoutException()
        {
        }

        public DynamicLayoutException(string message) : base(message)
        {
        }

        public DynamicLayoutException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}