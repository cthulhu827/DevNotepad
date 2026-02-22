using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public interface IApplication
    {
        BaseAppLayers Layers { get; }

        void Run();
    }
}
