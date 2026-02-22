using System;
using System.Collections.Generic;

namespace Framework.MVC
{
    public class EventRaiser<T>
    {
        private readonly Action<ICollection<T>> raiseEvent;

        private readonly ISet<T> changes = new HashSet<T>();

        private int raiseChangedRefCount;

        public EventRaiser(Action<ICollection<T>> raiseEvent)
        {
            this.raiseEvent = raiseEvent;
        }

        public void Raise(Action action, params T[] newChanges)
        {
            raiseChangedRefCount++;
            action();

            changes.AddRange(newChanges);

            raiseChangedRefCount--;
            if (raiseChangedRefCount != 0)
            {
                return;
            }

            raiseEvent(changes);
            changes.Clear();
        }
    }
}