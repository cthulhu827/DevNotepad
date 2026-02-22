using System;
using System.Linq;
using System.Collections.Generic;

namespace Framework.AppInfrastructure
{
    public class ObservableList<T> : IObservableList<T>
    {
        protected IList<T> items = new List<T>();

        private readonly IMessageBus messageBus = IoC.Resolve<IMessageBus>();

        private bool isUpdating;

        private ListChangedState changedState = ListChangedState.None;

        public IMessageBus MessageBus
        {
            get { return messageBus; }
        }

        public void AddItem(T item)
        {
            items.Add(item);
            SetListChanged();
        }

        public void InsertItem(int index, T item)
        {
            items.Insert(index, item);
            SetListChanged();
        }

        public void Clear()
        {
            items.Clear();
            SetListChanged();
        }

        public void BeginUpdate()
        {
            if (isUpdating)
            {
                throw new Exception("Twice called BeginUpdate()");
            }

            changedState = ListChangedState.None;
            isUpdating = true;
        }

        public void EndUpdate()
        {
            if (!isUpdating)
            {
                throw new Exception("BeginUpdate() was not called");
            }

            try
            {
                DoSendNotification(changedState);
            }
            finally
            {
                isUpdating = false;
            }
        }

        public IEnumerable<T> Items
        {
            get { return items; }
        }

        public int Count
        {
            get { return items.Count; }
        }

        public T this[int index]
        {
            get
            {
                return items[index];
            }
        }

        public void AddItems(IEnumerable<T> itemsToAdd)
        {
            items.AddRange(itemsToAdd);
            SetListChanged();
        }

        public void SortBy<TKey>(Func<T, TKey> keySelector)
        {
            items = items.OrderBy(keySelector).ToList();
            SetListChangedContents(-1);
        }

        public void SortByDesc<TKey>(Func<T, TKey> keySelector)
        {
            items = items.OrderByDescending(keySelector).ToList();
            SetListChangedContents(-1);
        }

        protected virtual void DoSendNotification(ListChangedState state)
        {
            switch (state)
            {
                case ListChangedState.None:
                    break;
                case ListChangedState.Contents:
                    messageBus.Notify_ListChangedContents();
                    break;
                case ListChangedState.Count:
                    messageBus.Notify_ListChanged();
                    break;
                default:
                    throw new AppException("Unsupported ListChangedState {0}", state.ToString());
            }
        }

        protected void SetListChanged()
        {
            if (isUpdating)
            {
                changedState = ListChangedState.Count;
            }
            else
            {
                DoSendNotification(ListChangedState.Count);
            }
        }

        protected void SetListChangedContents(int index)
        {
            if (isUpdating)
            {
                if (changedState != ListChangedState.Count)
                {
                    changedState = ListChangedState.Contents;
                }
            }
            else
            {
                messageBus.Notify_ListChangedContents(index);
            }
        }

        protected enum ListChangedState
        {
            None,
            Contents,
            Count
        }
    }
}
