using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public enum LayerType
    {
        None,
        Domain,
        BusinessLogic,
        UI,
        Persistence,
        Infrastructure,
        MainModule
    }
}
