using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class OneOfAttribute : TypeRefAttribute
    {
        public OneOfAttribute(Type type)
            : base(type)
        {
        }
    }
}
