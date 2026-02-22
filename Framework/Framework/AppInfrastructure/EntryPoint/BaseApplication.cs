using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public class BaseApplication : IApplication
    {
        public BaseAppLayers Layers { get; private set; }

        public virtual void Run()
        {
            DoConfigureAppLayers();
        }

        private void DoConfigureAppLayers()
        {
            Layers = new BaseAppLayers();

            DoAddAppLayers(Layers);

            Layers.ConfigIoC();
            Layers.Init();
        }

        protected virtual void DoAddAppLayers(BaseAppLayers layers)
        {
            // do nothing
        }
    }
}
