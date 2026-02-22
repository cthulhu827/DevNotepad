using System;
using System.Collections.Generic;

namespace Framework.AppInfrastructure
{
    [DefaultImplementation(typeof(IMessageBus))]
    internal class MessageBus : IMessageBus
    {
        private readonly IList<MethodRec> handlers = new List<MethodRec>();

        private int iterating;

        private readonly IList<Delegate> delayedAdditions = new List<Delegate>();
        private readonly IList<object> delayedRemovals = new List<object>();

        private void SignMethod(Delegate handler)
        {
            if (iterating != 0)
            {
                DelayAdd(handler);
            }
            else
            {
                handlers.Add(MethodRec.Create(handler));
            }
        }

        private void DelayAdd(Delegate handler)
        {
            delayedAdditions.Add(handler);
        }

        public void SignMethods(params Delegate[] handlers)
        {
            foreach (var handler in handlers)
            {
                SignMethod(handler);
            }
        }

        public void UnsignObject(object listener)
        {
            if (iterating != 0)
            {
                DelayRemove(listener);
            }
            else
            {
                for (int i = handlers.Count - 1; i >= 0; i--)
                {
                    var handler = handlers[i];
                    if (handler.Instance == listener)
                    {
                        handlers.Remove(handler);
                    }
                }
            }
        }

        private void DelayRemove(object listener)
        {
            delayedRemovals.Add(listener);
        }

        public void Notify(Type eventType, NotifyEvent evnt)
        {
            var paramList = new object[] { evnt };

            iterating++;
            try
            {
                foreach (var handler in handlers)
                {
                    if (handler.EventType == eventType)
                    {
                        handler.Method.Invoke(handler.Instance, paramList);
                    }
                }
            }
            finally
            {
                iterating--;
                if (iterating == 0)
                {
                    ApplyDelayedAdditions();
                    ApplyDelayedRemovals();
                }
            }
        }

        private void ApplyDelayedRemovals()
        {
            foreach (var obj in delayedRemovals)
            {
                UnsignObject(obj);
            }
            delayedRemovals.Clear();
        }

        private void ApplyDelayedAdditions()
        {
            foreach (var handler in delayedAdditions)
            {
                SignMethod(handler);
            }
            delayedAdditions.Clear();
        }

        public void Notify(NotifyEvent evnt)
        {
            Notify(evnt.GetType(), evnt);
        }

        public void NotifyNoParams<T>() where T : NotifyEvent
        {
            Notify(Activator.CreateInstance<T>());
        }

        public IMessageBus Sign<T>(On<T> handler) where T : NotifyEvent
        {
            SignMethod(handler);
            return this;
        }
    }
}
