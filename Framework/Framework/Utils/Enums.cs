using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace System
{
    public static class Enums
    {
        public static T[] Values<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .ToArray();
        }

        public static int[] IntValues<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T))
                .Cast<int>()
                .ToArray();
        }

        public static T GetNextEnumValue<T>(this T enumValue) where T : struct, IConvertible
        {
            var enumValues = Values<T>();

            if (enumValue.Equals(enumValues.Last()))
            {
                throw new Exception(string.Format("There is no next stage for {0}", enumValue));
            }

            return enumValues
                .SkipWhile(e => !e.Equals(enumValue))
                .Skip(1)
                .First();
        }

        public static T GetPrevEnumValue<T>(this T enumValue) where T : struct, IConvertible
        {
            var enumValues = Values<T>().Reverse().ToList();

            if (enumValue.Equals(enumValues.Last()))
            {
                throw new Exception(string.Format("There is no prev stage for {0}", enumValue));
            }

            return enumValues
                .SkipWhile(e => !e.Equals(enumValue))
                .Skip(1)
                .First();
        }

        public static string Description<T>(this T enumValue) where T : struct, IConvertible
        {
            var attr = GetValueAttribute<T, DescriptionAttribute>(enumValue);
            return attr == null ? enumValue.ToString() : attr.Description;
        }

        public static TAttr? GetValueAttribute<TEnum, TAttr>(TEnum enumValue)
            where TEnum : struct, IConvertible
            where TAttr : Attribute
        {
            var field = typeof(TEnum).GetField(enumValue.ToString());
            var attributes = (TAttr[])field.GetCustomAttributes(typeof(TAttr), false);
            return attributes.SingleOrDefault();
        }

        public static T ToEnum<T>(this int intValue) where T : struct, IConvertible
        {
            if (!Enum.IsDefined(typeof(T), intValue))
            {
                throw new AppException("Unknown enum {0} value {1}", typeof(T).Name, intValue);
            }

            return (T)(object)intValue;
        }

        public static T[] ToEnums<T>(this IEnumerable<int> intValues) where T : struct, IConvertible
        {
            return intValues
                .Select(value => value.ToEnum<T>())
                .ToArray();
        }

        public static int EnumToInt<T>(this T enumValue) where T : struct, IConvertible
        {
            return (int)(object)enumValue;
        }

        public static int[] ToInts<T>(this IEnumerable<T> enumValues) where T : struct, IConvertible
        {
            return enumValues
                .Select(value => (int)(object)value)
                .ToArray();
        }

        public static int ValueCount<T>() where T : struct, IConvertible
        {
            return Enum.GetValues(typeof(T)).Length;
        }
    }
}
