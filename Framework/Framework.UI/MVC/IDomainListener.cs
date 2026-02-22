using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.MVC
{
    public interface IDomainListener
    {
        void StartListeningDomain();
        void StopListeningDomain();
    }
}
