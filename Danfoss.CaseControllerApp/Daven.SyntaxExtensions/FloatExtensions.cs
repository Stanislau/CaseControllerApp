using System;

namespace Daven.SyntaxExtensions
{
    public static class FloatExtensions
    {
        public static bool In(this float value, float leftSide, float rightSide, bool includeLeft = true, bool includeRight = true)
        {
            return (includeLeft ? value >= leftSide : value > leftSide) && (includeRight ? value <= rightSide : value < rightSide);
        }

        public static bool IsValidNumber(this float value)
        {
            return float.IsInfinity(value) == false && float.IsNaN(value) == false;
        }

        public static bool IsEqualTo(this float value, float res, float delta = 0.001f)
        {
            return Math.Abs(value - res) < delta;
        }
    }
}