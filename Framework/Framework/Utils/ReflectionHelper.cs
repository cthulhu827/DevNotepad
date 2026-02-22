using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace System
{
    public static class ReflectionHelper
    {
        public static bool HasAttr<T>(this Type type, out T attribute) where T : Attribute
        {
            var attrs = type.GetCustomAttributes(typeof(T), false);
            bool result = attrs.Count() != 0;
            attribute = result ? (T)attrs[0] : null;
            return result;
        }

        public static bool HasAttr<T>(this Type type) where T : Attribute
        {
            T dummy;
            return HasAttr(type, out dummy);
        }

        public static bool HasAttr<T>(this Assembly assembly, out T attribute) where T : Attribute
        {
            var attrs = assembly.GetCustomAttributes(typeof(T), false);
            bool result = attrs.Count() != 0;
            attribute = result ? (T)attrs[0] : null;
            return result;
        }

        public static bool HasAttr<T>(this Assembly assembly) where T : Attribute
        {
            T dummy;
            return HasAttr(assembly, out dummy);
        }
    }
}
