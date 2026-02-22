using System;

namespace Framework.AppInfrastructure
{
    public class NotifyEvent : IDisposable
    {
        public void Dispose()
        {
            if (OnDispose != null)
            {
                OnDispose(this);
            }
        }

        public event Action<NotifyEvent> OnDispose;
    }
}
