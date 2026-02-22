using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImplementsAttribute : Attribute, ITypeRefAttribute
    {
        public ImplementsAttribute(params Type[] types)
        {
            Types = types;
        }

        public Type[] Types { get; private set; }

        public bool ContainsType(Type type)
        {
            return Types.Contains(type);
        }
    }
}
