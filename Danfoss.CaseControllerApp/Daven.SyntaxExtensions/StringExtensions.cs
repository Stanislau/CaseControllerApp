using System;
using System.Collections.Generic;
using System.Linq;

namespace Daven.SyntaxExtensions
{
    public static class StringExtensions
    {
        public static bool IsExist(this string self) => string.IsNullOrEmpty(self) == false;

        public static bool IsNullOrEmptyString(this string self) => string.IsNullOrEmpty(self);

        public static bool IsNullOrEmptyStringOrWhiteSpaces(this string self) => string.IsNullOrEmpty(self) || string.IsNullOrWhiteSpace(self);

        public static bool IsWithoutInformation(this string self) => self.IsNullOrEmptyStringOrWhiteSpaces();

        public static bool IsNullOrWhiteSpace(this string self) => string.IsNullOrWhiteSpace(self);

        public static string FormatWith(this string self, params object[] args) => string.Format(self, args);

        public static string JoinStrings(this IEnumerable<string> strings, string separator = ", ") => string.Join(separator, strings);

        public static string PrepareForComparison(this string value) => value.Trim().ToLowerInvariant();

        public static bool IsOneOf(this string value, params string[] items) => items.Any(x => x == value);

        public static string If(this string value, bool condition) => condition ? value : String.Empty;

        public static string SelfOrEmpty(this string value) => value.IsExist() ? value : string.Empty;

        public static string SelfOr(this string value, string another) => value.IsExist() ? value : another;

        public static string BreakOnWordAfterSumbol(this string value, int maxLength, int minWords = 1)
        {
            if (value.Length > maxLength)
            {
                var words = value.Split(' ');

                var result = string.Empty;
                for (int i = 0; i < words.Length; i++)
                {
                    var old = result;
                    result += words[i];
                    if (result.Length > maxLength)
                    {
                        if (i < minWords)
                        {
                            return value.Substring(0, maxLength);
                        }
                        return old.TrimEnd() + "...";
                    }
                    else
                    {
                        result += ' ';
                    }
                }
            }
            return value;
        }
    }
}