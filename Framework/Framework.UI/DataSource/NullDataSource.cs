using Framework.AppInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Domain;
using Framework.UI;

namespace Framework.MVC
{
    internal class NullDataSource<T> : IDataSource<T> where T : ViewModel
    {
        private readonly IMessageBus messageBus = BaseNull._<IMessageBus>();
        public IMessageBus MessageBus { get { return messageBus; } }

        public T this[int index]
        {
            get { throw new IndexOutOfRangeException(); }
        }

        public void AddItem(T item)
        {
            // do nothing
        }

        public void InsertItem(int index, T item)
        {
            // do nothing
        }

        public bool RemoveItem(int itemId)
        {
            return false;
        }

        public void InsertBefore(int itemId, T item)
        {
            // do nothing
        }

        public void Clear()
        {
            // do nothing
        }

        public T ById(int itemId, out int index)
        {
            index = -1;
            return null;
        }

        public T ById(int itemId)
        {
            int dummy;
            return ById(itemId, out dummy);
        }

        public void BeginUpdate()
        {
            // do nothing
        }

        public void EndUpdate()
        {
            // do nothing
        }

        public int Count
        {
            get { return 0; }
        }

        public IEnumerable<T> Items
        {
            get { return Enumerable.Empty<T>(); }
        }

        public void FillFromDomain<TDomain>(IEnumerable<TDomain> entities, BuildViewModelDelegate<T, TDomain> buildVm) where TDomain : Entity
        {
            // do nothing
        }

        public void AddItems(IEnumerable<T> itemsToAdd)
        {
            // do nothing
        }

        public void SortBy<TKey>(Func<T, TKey> keySelector)
        {
            // do nothing
        }

        public void SortByDesc<TKey>(Func<T, TKey> keySelector)
        {
            // do nothing
        }
    }
}
