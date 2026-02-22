using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Domain;

namespace Framework.AppInfrastructure
{
    public class NEListChanged : NotifyEvent
    {
        public NotifyEvent InternalEvent;
    }
}
