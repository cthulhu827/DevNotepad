using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.AppInfrastructure
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultImplementationAttribute : Attribute, ITypeRefAttribute
    {
        public DefaultImplementationAttribute(IoCMode iocMode, params Type[] types)
        {
            Types = (iocMode == IoCMode.ProdOnly && UnitTests.IsTestEnvironment) ||
                    (iocMode == IoCMode.TestOnly && !UnitTests.IsTestEnvironment)
                        ? new Type[0]
                        : types;
        }

        public DefaultImplementationAttribute(params Type[] types)
            : this(IoCMode.Any, types)
        {
        }

        public Type[] Types { get; private set; }

        public bool ContainsType(Type type)
        {
            return Types.Contains(type);
        }
    }
}