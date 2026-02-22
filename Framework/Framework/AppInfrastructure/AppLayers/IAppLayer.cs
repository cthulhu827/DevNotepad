using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.AppInfrastructure
{
    public interface IAppLayer
    {
        void InitLayer();
        void ConfigIoC();
        LayerType LayerType { get; }
        Assembly Assembly { get; }
    }
}
