using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.AppInfrastructure
{
    public interface IObservableList<T> : IObservableCollection<T>
    {
        void AddItem(T item);
        void InsertItem(int index, T item);
        void Clear();

        void AddItems(IEnumerable<T> itemsToAdd);
        void BeginUpdate();
        void EndUpdate();

        void SortBy<TKey>(Func<T, TKey> keySelector);
        void SortByDesc<TKey>(Func<T, TKey> keySelector);
    }
}
