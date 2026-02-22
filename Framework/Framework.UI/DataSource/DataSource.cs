using System.Linq;
using Framework.AppInfrastructure;
using System;
using System.Collections.Generic;
using Framework.Domain;
using Framework.UI;

namespace Framework.MVC
{
    public class DataSource<T> : ObservableList<T>, IDataSource<T>, IDomainListener where T : ViewModel
    {
        public bool RemoveItem(int itemId)
        {
            T tmp = ById(itemId);

            if (tmp != null)
            {
                items.Remove(tmp);
                SetListChanged();
                return true;
            }

            return false;
        }

        public void InsertBefore(int itemId, T item)
        {
            int index;
            var dummy = ById(itemId, out index);
            if (dummy == null)
            {
                throw new AppException("Not found item with Id = {0}", itemId);
            }

            items.Insert(index, item);
            SetListChanged();
        }

        public T ById(int itemId, out int index)
        {
            for (int i = 0; i < items.Count; i++)
            {
                var vm = this[i];
                if (vm.Id == itemId)
                {
                    index = i;
                    return vm;
                }
            }

            index = -1;
            return null;
        }

        public T ById(int itemId)
        {
            int dummy;
            return ById(itemId, out dummy);
        }

        public void FillFromDomain<TDomain>(IEnumerable<TDomain> entities, BuildViewModelDelegate<T, TDomain> buildVm) where TDomain : Entity
        {
            BeginUpdate();
            try
            {
                Clear();
                AddItems(entities.Select(domainEntity => buildVm(domainEntity)));
            }
            finally
            {
                EndUpdate();
            }
        }

        protected virtual void DoStartListeningDomain()
        {
            // do nothing
        }

        protected virtual void DoStopListeningDomain()
        {
            // do nothing
        }

        private int domainListenerRefCount = -1;

        public void StartListeningDomain()
        {
            domainListenerRefCount++;
            if (domainListenerRefCount == 0)
                DoStartListeningDomain();
        }

        public void StopListeningDomain()
        {
            domainListenerRefCount--;
            if (domainListenerRefCount == -1)
                DoStopListeningDomain();
        }
    }
}
