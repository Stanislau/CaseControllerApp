using System;

namespace Daven.SyntaxExtensions
{
    public static class ActionHelper
    {
        public static Action Empty => () => { };
    }
}