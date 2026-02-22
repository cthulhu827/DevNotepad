using Framework.AppInfrastructure;
using System;
using System.Linq;

namespace Framework.UI
{
    [NullTypeFor(typeof(ActionScope))]
    public class ActionScope
    {
        public void CheckSupports<T>()
        {
            CheckSupports(typeof(T));
        }

        public void CheckSupports(Type type)
        {
            Type[] interfaces = this.GetType().GetInterfaces();
            if (!interfaces.Contains(type))
                throw new AppException("Class {0} must implement {1} interface", this.GetType().Name, type.Name);
        }

        public T As<T>() where T : class, IAS_Base
        {
            return this as T;
        }
    }
}
