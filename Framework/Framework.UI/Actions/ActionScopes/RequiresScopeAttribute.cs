using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public sealed class RequiresScopeAttribute : Attribute
    {
        public Type ScopeInterface { get; private set; }

        public RequiresScopeAttribute(Type scopeInterface)
        {
            ScopeInterface = scopeInterface;
        }
    }
}
