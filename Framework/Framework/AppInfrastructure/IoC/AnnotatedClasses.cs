using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Framework.AppInfrastructure
{
    public static class AnnotatedClasses
    {
        private static IEnumerable<Type> GetTypesWith<T>(Type type, Assembly assembly) where T : Attribute, ITypeRefAttribute
        {
            var result = new List<Type>();

            if (assembly.HasAttr<SearchAnnotatedClassesAttribute>())
            {
                result.AddRange(assembly.GetTypes().Where(attrType =>
                {
                    T attr;
                    return (attrType.HasAttr(out attr)) && (attr.ContainsType(type));
                }));
            }

            return result;
        }

        private static ICollection<Type> GetTypesWith<T>(Type type) where T : Attribute, ITypeRefAttribute
        {
            var result = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetTypesWith<T>(type, assembly));
            }

            return result;
        }

        private static IEnumerable<Type> GetTypesWith<T>(Assembly assembly) where T : Attribute
        {
            var result = new List<Type>();

            if (assembly.HasAttr<SearchAnnotatedClassesAttribute>())
            {
                result.AddRange(assembly.GetTypes().Where(attrType =>
                {
                    T attr;
                    return (attrType.HasAttr(out attr));
                }));
            }

            return result;
        }

        private static Type SingleTypeWith<T>(Type type, string errorText) where T : Attribute, ITypeRefAttribute
        {
            var implTypes = GetTypesWith<T>(type);

            if (implTypes.IsEmpty())
            {
                return null;
            }

            if (implTypes.Count() > 1)
            {
                throw new AppException(errorText, type.Name);
            }

            return implTypes.Single();
        }

        public static ICollection<Type> GetTypesWith<T>() where T : Attribute
        {
            var result = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                result.AddRange(GetTypesWith<T>(assembly));
            }

            return result;
        }

        public static IEnumerable<Type> AllOf(Type type, Assembly assembly)
        {
            return GetTypesWith<OneOfAttribute>(type, assembly);
        }

        public static IEnumerable<Type> AllOf(Type type)
        {
            return GetTypesWith<OneOfAttribute>(type);
        }

        public static IEnumerable<Type> AllOf<T>(Assembly assembly)
        {
            return AllOf(typeof(T), assembly);
        }

        public static IEnumerable<Type> AllOf<T>()
        {
            return AllOf(typeof(T));
        }

        /// <summary>
        /// Находит все классы, реализующие указанный интерфейс.
        /// </summary>
        public static IEnumerable<Type> AllImplementing<TInterface>()
        {
            var interfaceType = typeof(TInterface);
            var result = new List<Type>();

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.HasAttr<SearchAnnotatedClassesAttribute>())
                {
                    result.AddRange(assembly.GetTypes().Where(type =>
                        type.IsClass &&
                        !type.IsAbstract &&
                        interfaceType.IsAssignableFrom(type)));
                }
            }

            return result;
        }

        public static Type ImplementaionOf(Type type)
        {
            return SingleTypeWith<ImplementsAttribute>(type, "More than one implementation for {0}")
                ?? SingleTypeWith<DefaultImplementationAttribute>(type, "More than one default implementation for {0}");
        }

        public static Type NullTypeFor(Type type)
        {
            return SingleTypeWith<NullTypeForAttribute>(type, "More than one null type for {0}");
        }

        /// <summary>
        /// Все интерфейсы, которые имплементированы в <paramref name="implType"/>
        /// через атрибуты <see cref="ImplementsAttribute"/> и <see cref="DefaultImplementationAttribute"/>.
        /// Также добавляет к результату интерфейсы <paramref name="interfaceTypesToAdd"/>; это нужно для
        /// случаев, когда исходный интерфейс, по которому найден класс-имплементор, сам не фигурирует
        /// у этого класса в атрибутах, а зарегистрирован, например, через IoC.Register.
        /// </summary>
        public static IEnumerable<Type> GetImplementedInterfaces(this Type implType, params Type[] interfaceTypesToAdd)
        {
            var result = new List<Type>();

            ImplementsAttribute implementsAttr;
            if (implType.HasAttr(out implementsAttr))
            {
                result.AddRange(implementsAttr.Types);
            }

            DefaultImplementationAttribute defaultImplementationAttr;
            if (implType.HasAttr(out defaultImplementationAttr))
            {
                result.AddRange(defaultImplementationAttr.Types);
            }

            return result
                .Union(interfaceTypesToAdd);
        }
    }
}
