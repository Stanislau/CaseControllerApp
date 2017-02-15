using System;
using System.Linq;

namespace Daven.SyntaxExtensions
{
    public static class Int32Extensions
    {
        public static bool In(this int value, int leftSide, int rightSide, bool includeLeft = true,
            bool includeRight = true)
        {
            return (includeLeft ? value >= leftSide : value > leftSide) && (includeRight ? value <= rightSide : value < rightSide);
        }

        public static bool In(this int? value, int leftSide, int rightSide, bool includeLeft, bool includeRight)
        {
            if (value.HasValue)
            {
                return value.Value.In(leftSide, rightSide, includeLeft, includeRight);
            }
            return false;
        }

        public static bool IsInSet(this int value, params int[] set) => set.Any(i => i == value);

        public static bool IsInSet(this int? value, params int[] set)
        {
            if (value.HasValue)
            {
                return value.Value.IsInSet(set);
            }
            return false;
        }

        public static int SelfOr(this int? value, int replace)
        {
            return value.HasValue ? value.Value : replace;
        }

        public static int ParseToInt32(this string value)
        {
            return Int32.Parse(value);
        }
    }
}