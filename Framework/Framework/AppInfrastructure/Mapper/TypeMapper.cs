using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public class TypeMapper : Mapper<Type, Type>
    {
        private readonly bool searchByAttr;

        public TypeMapper(bool searchByAttr)
            : base()
        {
            this.searchByAttr = searchByAttr;
        }

        public void Register<TItf, TImpl>()
        {
            Register(typeof(TItf), typeof(TImpl));
        }

        public override Type Get(Type itfType, bool throwIfNotFound = true)
        {
            if (!searchByAttr)
            {
                return base.Get(itfType, throwIfNotFound);
            }

            var result = base.Get(itfType, false);
            if (result == null)
            {
                result = AnnotatedClasses.ImplementaionOf(itfType);

                if (result != null)
                {
                    // Класс может имплементировать не только запрошенный интерфейс,
                    // но и другие - их тоже сразу регистрируем у себя.
                    var implementedInterfaces = result.GetImplementedInterfaces(itfType);
                    foreach (var itf in implementedInterfaces)
                    {
                        Register(itf, result);
                    }
                }
            }

            if (result == null && throwIfNotFound)
            {
                throw new AppException("Can't resolve type {0}", itfType.Name);
            }
            
            return result;
        }

        public Type Get<T>(bool throwIfNotFound = true)
        {
            return Get(typeof(T), throwIfNotFound);
        }
    }
}
