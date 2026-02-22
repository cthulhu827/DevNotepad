using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public delegate void On<in T>(T evnt) where T : NotifyEvent;

    public interface IMessageBus
    {
        void SignMethods(params Delegate[] handlers);
        IMessageBus Sign<T>(On<T> handler) where T : NotifyEvent;
        void UnsignObject(object listener);
        void Notify(Type eventType, NotifyEvent evnt);
        void Notify(NotifyEvent evnt);
        void NotifyNoParams<T>() where T : NotifyEvent;
    }
}
