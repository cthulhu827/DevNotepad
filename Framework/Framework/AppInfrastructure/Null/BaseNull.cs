using System;
using System.Linq;
using System.Collections.Generic;

namespace Framework.AppInfrastructure
{
    public class BaseNull
    {
        private static readonly IDictionary<Type, object> NullObjects = new Dictionary<Type, object>();
        private static readonly IDictionary<Type, Type> NullTypes = new Dictionary<Type, Type>();

        public static T _<T>()
        {
            var resolveType = typeof(T);
            object result;

            if (!NullObjects.TryGetValue(resolveType, out result))
            {
                Type nullType;
                if (!NullTypes.TryGetValue(resolveType, out nullType))
                {
                    nullType = AnnotatedClasses.NullTypeFor(resolveType);
                }

                if (nullType == null)
                {
                    throw new AppException("No null type for {0} type", resolveType.Name);
                }

                result = Activator.CreateInstance(nullType);

                NullObjects.Add(resolveType, result);

                if (result is INullObjectSetup)
                {
                    (result as INullObjectSetup).SetupNullObject();
                }
            }

            return (T)result;
        }

        public static void RegisterNullType<TResolveType, TNullType>()
        {
            RegisterNullType(typeof(TResolveType), typeof(TNullType));
        }

        public static void RegisterNullType(Type resolveType, Type nullType)
        {
            NullTypes.Add(resolveType, nullType);
        }

        public static void RegisterNullObject<TResolve>(object nullObject)
        {
            NullObjects.Add(typeof(TResolve), nullObject);

            if (nullObject is INullObjectSetup)
            {
                (nullObject as INullObjectSetup).SetupNullObject();
            }
        }
    }
}

