using System;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.AppInfrastructure
{
    public class BaseAppLayer : IAppLayer
    {
        public virtual void ConfigIoC()
        {
            // do nothing
        }

        public virtual void InitLayer()
        {
            // do nothing
        }

        public LayerType LayerType { get; private set; }

        public BaseAppLayer(LayerType layerType)
        {
            LayerType = layerType;
        }

        public Assembly Assembly
        {
            get { return GetType().Assembly; }
        }

        // ReSharper disable UnusedTypeParameter
        public BaseAppLayer ForceLoad<T>() where T : AbstractAssemblyReference
        {
            return this;
        }
        // ReSharper restore UnusedTypeParameter
    }
}
