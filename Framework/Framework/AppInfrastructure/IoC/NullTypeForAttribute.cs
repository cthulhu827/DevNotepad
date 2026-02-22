using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class NullTypeForAttribute : TypeRefAttribute
    {
        public NullTypeForAttribute(Type type)
            : base(type)
        {
        }
    }
}
