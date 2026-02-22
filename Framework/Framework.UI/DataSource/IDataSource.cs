using Framework.AppInfrastructure;
using System.Collections.Generic;
using Framework.Domain;
using Framework.UI;

namespace Framework.MVC
{
    public delegate TViewModel BuildViewModelDelegate<out TViewModel, in TDomain>(TDomain entity)
        where TViewModel : ViewModel
        where TDomain : Entity;

    public interface IDataSource<T> : IObservableList<T> where T : ViewModel
    {
        bool RemoveItem(int itemId);

        void InsertBefore(int itemId, T item);

        T? ById(int itemId, out int index);

        T? ById(int itemId);

        void FillFromDomain<TDomain>(IEnumerable<TDomain> entities, BuildViewModelDelegate<T, TDomain> buildVm) where TDomain : Entity;
    }
}
