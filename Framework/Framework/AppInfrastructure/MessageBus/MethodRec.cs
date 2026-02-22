using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Framework.AppInfrastructure
{
    public class MethodRec
    {
        public Type EventType;
        public object Instance;
        public MethodInfo Method;

        public static MethodRec Create(Delegate handler)
        {
            Type eventType = handler.GetType().GetGenericArguments()[0];
            object instance = handler.Target;
            MethodInfo method = handler.Method;

            return new MethodRec { EventType = eventType, Instance = instance, Method = method };
        }
    }
}
