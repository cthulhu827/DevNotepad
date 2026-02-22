using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public class TypeRefAttribute : Attribute, ITypeRefAttribute
    {
        public Type Type { get; private set; }

        public TypeRefAttribute(Type type)
            : base()
        {
            Type = type;
        }

        public bool ContainsType(Type type)
        {
            return Type == type;
        }
    }
}
