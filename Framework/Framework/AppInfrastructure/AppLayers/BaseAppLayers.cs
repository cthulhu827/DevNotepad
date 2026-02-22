using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Framework.AppInfrastructure
{
    public class BaseAppLayers
    {
        private readonly IList<IAppLayer> layers = new List<IAppLayer>();

        private Type GetAppLayerImplementorClass(Assembly assembly)
        {
            return AnnotatedClasses.AllOf<IAppLayer>(assembly).FirstOrDefault();
        }

        public BaseAppLayers AddLayer<T>(LayerType layerType) where T : AbstractAssemblyReference
        {
            var implementorClass = GetAppLayerImplementorClass(typeof(T).Assembly);
            if (implementorClass != null)
            {
                var layer = (IAppLayer)Activator.CreateInstance(implementorClass, layerType);
                layers.Add(layer);
            }

            return this;
        }

        public void ConfigIoC()
        {
            foreach (var layer in layers)
            {
                layer.ConfigIoC();
            }
        }

        public void Init()
        {
            foreach (var layer in layers)
            {
                layer.InitLayer();
            }
        }

        public IEnumerable<IAppLayer> ByType(LayerType layerType)
        {
            var result = layers.Where(layer => layer.LayerType == layerType).ToArray();

            if (result.IsEmpty())
            {
                throw new AppException("Not found layer with LayerType={0}", layerType);
            }

            return result;
        }
    }
}
