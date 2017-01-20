using System;
using System.Linq;

namespace Daven.SyntaxExtensions
{
    public static class EnumExtensions
    {
        public static bool HasFlags(this Enum self, params Enum[] targets)
        {
            return targets.All(self.HasFlag);
        }

        public static bool HasNoFlag(this Enum self, Enum target)
        {
            return self.HasFlag(target) == false;
        }

        public static bool HasNoFlags(this Enum self, params Enum[] targets)
        {
            return targets.All(self.HasNoFlag);
        }

        public static T Add<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type | (int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not append value from enumerated type '{typeof (T).Name}'.", ex);
            }
        }


        public static T Remove<T>(this System.Enum type, T value)
        {
            try
            {
                return (T)(object)(((int)(object)type & ~(int)(object)value));
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Could not remove value from enumerated type '{typeof (T).Name}'.", ex);
            }
        }

        public static bool IsOneOf<T>(this System.Enum type, params T[] values)
        {
            //[sk]: do not tested
            return values.Any(value => type.Equals(value));
        }
    }
}