using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Daven.SyntaxExtensions
{
    public static class EnumerableExtensions
    {
        public static T MaxFirstItem<T>(this IEnumerable<T> items, Func<T, int> getProperty, out int maxDamage)
        {
            if (items.Any() == false)
            {
                maxDamage = 0;
                return default(T);
            }

            maxDamage = getProperty(items.First());
            T maxItem = items.First();
            foreach (var item in items)
            {
                var itemInt = getProperty(item);
                if (maxDamage < itemInt)
                {
                    maxDamage = itemInt;
                    maxItem = item;
                }
            }
            return maxItem;
        }

        public static bool NoOne<T>(this IEnumerable<T> items, Func<T, bool> filter)
        {
            return items.Any(filter) == false;
        }

        public static T ElementOfTypeAt<T>(this IEnumerable items, int skip)
        {
            foreach (var item in items)
            {
                if (item is T)
                {
                    if (skip > 0)
                    {
                        skip--;
                        continue;
                    }
                    else
                    {
                        return item.CastInstanceTo<T>();
                    }
                }
            }
            return default(T);
        }

        public static T FirstOf<T>(this IEnumerable items)
        {
            foreach (var item in items)
            {
                if (item is T)
                {
                    return item.CastInstanceTo<T>();
                }
            }
            return default(T);
        }

        public static long MaxOrDefault<T>(this IEnumerable<T> items, Func<T, long> selector)
        {
            if (items.IsNullOrEmpty()) return 0;
            return items.Max(selector);
        }

        public static bool DoesNotContains<T>(this IEnumerable<T> self, Func<T, bool> predicate)
        {
            return self.Any(predicate) == false;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> self)
        {
            return self == null || self.Any() == false;
        }

        public static bool AreFilledIn<T>(this IEnumerable<T> self)
        {
            return self.IsNullOrEmpty() == false;
        }

        public static bool IsNullOrEmpty(this IEnumerable self)
        {
            return self == null || Enumerable.Cast<object>(self).Any() == false;
        }

        public static bool ContainsNull<T>(this IEnumerable<T> self) where T : class
        {
            return self == null || self.Any(x => x == null);
        }

        public static IEnumerable<T> SelfOrEmpty<T>(this IEnumerable<T> self)
        {
            return self ?? Enumerable.Empty<T>();
        }

        public static IEnumerable<object> ToTypedEnumerable(this IEnumerable self)
        {
            return Enumerable.Cast<object>(self);
        }

        public static bool IsEmpty<T>(this IEnumerable<T> self)
        {
            return self.Any() == false;
        }

        public static int IndexOf<T>(this IEnumerable<T> self, Func<T, bool> condition)
        {
            var i = 0;

            foreach (var item in self)
            {
                if (condition(item))
                {
                    return i;
                }
                i++;
            }

            return -1;
        }

        public static bool EqualsTo<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null && second != null) return false;
            if (first != null && second == null) return false;
            if (first == null && second == null) return true;

            var firstArray = first.ToArray();
            var secondArray = second.ToList();

            if (firstArray.Length != secondArray.Count) return false;

            foreach (var f in firstArray)
            {
                if (secondArray.RemoveAll(x => x.Equals(f)) != 1)
                {
                    return false;
                }
            }

            return secondArray.Count == 0;
        }

        public static IEnumerable<T> PerformAction<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var v in self)
            {
                action(v);
                yield return v;
            }
        }

        public static IEnumerable<T2> WhereItemIs<T2>(this IEnumerable<object> self)
        {
            return self.Where(x => x is T2).Select(x => x.CastInstanceTo<T2>());
        }

        public static int IndexOf<T>(this IEnumerable<T> self, T item)
        {
            var index = 0;
            foreach (var i in self)
            {
                if (i.Equals(item))
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        public static T NextTo<T>(this IEnumerable<T> self, T current) where T: class
        {
            if (current == null) return null;

            bool findCurrent = false;
            foreach (var o in self)
            {
                if (findCurrent)
                {
                    return o;
                }

                findCurrent = o.Equals(current);
            }
            return null;
        }

        public static T PrevTo<T>(this IEnumerable<T> self, T current) where T: class
        {
            if (current == null) return null;

            T prev = null;
            foreach (var o in self)
            {
                if (o.Equals(current))
                {
                    return prev;
                }
                prev = o;
            }
            return null;
        }

        public static IEnumerable<T> Include<T>(this IEnumerable<T> self, params T[] items)
        {
            foreach (var s in self)
            {
                yield return s;
            }

            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> InstanseAsEnumerable<T>(this T self)
        {
            return self == null ? Enumerable.Empty<T>() : new[] {self};
        }

        public static IEnumerable<T> GetReversed<T>(this IEnumerable<T> items)
        {
            var list = items.ToList();
            list.Reverse();
            return list;
        }
    }
}