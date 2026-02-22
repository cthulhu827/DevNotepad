using System;
using System.Linq;
using System.Collections.Generic;

namespace Framework.AppInfrastructure
{
    public static class IoC
    {
        private static ISet<Type> typesResolvedAsSingleton = new HashSet<Type>();

        private static TypeMapper implementingTypes = new TypeMapper(true);

        private static Mapper<Type, object> implementingConsts = new Mapper<Type, object>();

        private static Mapper<Type, Delegate> implementingMethods = new Mapper<Type, Delegate>();

        private static Mapper<Type, object> temporaryBindings = new Mapper<Type, object>();

        public static TItf Resolve<TItf>()
        {
            return (TItf)Resolve(typeof(TItf));
        }

        public static object Resolve(Type itfType)
        {
            // Временные баиндинги должны резолвиться всегда в первую очередь,
            // чтобы иметь приоритет над всеми остальными. В том числе возможна
            // ситуация, когда какой-то интерфейс уже разресолвлен каким-то
            // другим способом (например, через атрибут Implements). Тогда, если
            // определён временный баиндинг, то будет действовать он, а когда
            // этот баиндинг будет отключен (UnregisterTemporary), то опять вступит
            // в действие реализация по умолчанию (определённая через Implements).
            var implTemporary = temporaryBindings.Get(itfType, false);
            if (implTemporary != null)
            {
                return implTemporary;
            }

            var implConst = implementingConsts.Get(itfType, false);
            if (implConst != null)
            {
                return implConst;
            }

            var implMethod = implementingMethods.Get(itfType, false);
            if (implMethod != null)
            {
                var result = ((Func<object>)implMethod)();

                if (typesResolvedAsSingleton.Contains(itfType))
                {
                    implementingConsts.Register(itfType, result);
                }

                return result;
            }

            var implType = implementingTypes.Get(itfType, false);
            if (implType != null)
            {
                object result;
                try
                {
                    result = Activator.CreateInstance(implType);
                }
                catch (Exception e)
                {
                    throw new TypeResolveException(itfType, implType, e);
                }

                if (typesResolvedAsSingleton.Contains(itfType) || implType.HasAttr<SingletonAttribute>())
                {
                    // Класс может имплементировать не только запрошенный интерфейс,
                    // но и другие - их тоже сразу регистрируем у себя.
                    var implementedInterfaces = result.GetType().GetImplementedInterfaces(itfType);
                    foreach (var itf in implementedInterfaces)
                    {
                        implementingConsts.Register(itf, result);
                    }
                }

                return result;
            }

            throw new AppException("Can't resolve type {0}", itfType.Name);
        }

        public static void Bind<TItf, TImpl>(bool singleton = false)
        {
            implementingTypes.Register<TItf, TImpl>();

            if (singleton)
            {
                typesResolvedAsSingleton.Add(typeof(TItf));
            }
        }

        public static void BindToMethod<TItf>(Func<TItf> instanceCreator, bool singleton = false)
        {
            implementingMethods.Register(typeof(TItf), instanceCreator);

            if (singleton)
            {
                typesResolvedAsSingleton.Add(typeof(TItf));
            }
        }

        public static void BindToConst<TItf>(TItf constant)
        {
            implementingConsts.Register(typeof(TItf), constant);
        }

        public static void BindTemporary<TItf>(TItf constant)
        {
            temporaryBindings.Register(typeof(TItf), constant);
        }

        public static void UnbindTemporary<TItf>()
        {
            temporaryBindings.Unregister(typeof(TItf));
        }

        public static void Reset()
        {
            typesResolvedAsSingleton = new HashSet<Type>();
            implementingTypes = new TypeMapper(true);
            implementingConsts = new Mapper<Type, object>();
            implementingMethods = new Mapper<Type, Delegate>();
            temporaryBindings = new Mapper<Type, object>();
        }
    }
}
