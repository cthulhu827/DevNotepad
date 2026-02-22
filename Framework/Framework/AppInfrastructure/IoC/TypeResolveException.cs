using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    internal class TypeResolveException : Exception
    {
        private static string ResolveMsg(Type itfType, Type implType)
        {
            return string.Format("\n### Resolve {0} -> {1} ###\n", itfType.Name, implType.Name);
        }

        public TypeResolveException(Type itfType, Type implType, Exception innerException)
            : base(ResolveMsg(itfType, implType), innerException)
        {
        }
    }
}
