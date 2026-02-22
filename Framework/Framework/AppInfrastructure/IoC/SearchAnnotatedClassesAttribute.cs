using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public sealed class SearchAnnotatedClassesAttribute : Attribute
    {
    }
}
