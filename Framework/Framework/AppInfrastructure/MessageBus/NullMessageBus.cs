using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    [NullTypeFor(typeof(IMessageBus))]
    class NullMessageBus : IMessageBus
    {
        public void SignMethods(params Delegate[] handlers)
        {
            // do nothing
        }

        public void UnsignObject(object listener)
        {
            // do nothing
        }

        public void Notify(Type eventType, NotifyEvent evnt)
        {
            // do nothing
        }

        public void Notify(NotifyEvent evnt)
        {
            // do nothing
        }


        public void NotifyNoParams<T>() where T : NotifyEvent
        {
            // do nothing
        }

        public IMessageBus Sign<T>(On<T> handler) where T : NotifyEvent
        {
            return this;
        }
    }
}
