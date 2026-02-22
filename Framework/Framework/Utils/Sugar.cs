using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace System
{
    public static class Sugar
    {
        public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            return !enumerable.Any(predicate);
        }

        public static bool In<T>(this T theValue, params T[] values)
        {
            return values.Contains(theValue);
        }

        public static bool NotIn<T>(this T theValue, params T[] values)
        {
            return !values.Contains(theValue);
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var item in enumerable)
            {
                action(item);
            }
        }

        public static IEnumerable<T> AsEnumerable<T>(this T value)
        {
            return new[] { value };
        }

        public static T[] AsArray<T>(this T value)
        {
            return new[] { value };
        }

        public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !enumerable.Any();
        }

        public static string Join(this IEnumerable<string> strings, string separator = ", ")
        {
            return string.Join(separator, strings.ToArray());
        }

        public static int IndexOf<T>(this IList<T> list, Func<T, bool> predicate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (predicate(list[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public static void AddRange<T>(this ICollection<T> list, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                list.Add(item);
            }
        }

        public static void AddTo<T>(this IEnumerable<T> collection, ICollection<T> targetList)
        {
            foreach (var item in collection)
            {
                targetList.Add(item);
            }
        }

        public static T MaxSafe<T>(this IEnumerable<T> collection, T defaultValue)
        {
            if (collection == null)
            {
                return defaultValue;
            }

            var array = collection.ToArray();
            return array.IsEmpty() ? defaultValue : array.Max();
        }

        public static T MinSafe<T>(this IEnumerable<T> collection, T defaultValue)
        {
            if (collection == null)
            {
                return defaultValue;
            }

            var array = collection.ToArray();
            return array.IsEmpty() ? defaultValue : array.Min();
        }

        public static bool IsEmpty(this string s)
        {
            return string.IsNullOrWhiteSpace(s);
        }

        public static bool IsNotEmpty(this string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }

        public static bool ContainsIgnoreCase(this string s, string textToSearch)
        {
            return s.IsNotEmpty() &&
                textToSearch.IsNotEmpty() &&
                s.IndexOf(textToSearch, StringComparison.InvariantCultureIgnoreCase) != -1;
        }

        public static ISet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        public static void Set<T>(this IList<T> list, IEnumerable<T> newCollection)
        {
            list.Clear();
            list.AddRange(newCollection);
        }

        public static void SetTo<T>(this IEnumerable<T> newCollection, IList<T> targetList)
        {
            targetList.Clear();
            targetList.AddRange(newCollection);
        }

        public static int ToInt(this bool b)
        {
            return b ? 1 : 0;
        }

        public static bool ToBool(this int i)
        {
            switch (i)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Обрезает <paramref name="itemsToTrim"/> последних элементов коллекции
        /// </summary>
        public static T[] TrimLast<T>(this ICollection<T> src, int itemsToTrim = 1)
        {
            return src.Take(src.Count - itemsToTrim).ToArray();
        }

        public static V GetOrDefault<K, V>(this IDictionary<K, V> dictionary, K key, V defaultValue)
        {
            V result;
            return dictionary.TryGetValue(key, out result) ? result : defaultValue;
        }

        public static string Int(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }

        public static string IntOrEmpty(this int value)
        {
            return value == 0 ? string.Empty : value.Int();
        }
    }
}
