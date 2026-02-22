using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Domain;

namespace Framework.AppInfrastructure
{
    public class NEListChangedContents : NotifyEvent
    {
        public int ItemIndex;
        public NotifyEvent InternalEvent;

        public NEListChangedContents()
        {
            ItemIndex = -1;
        }
    }
}
