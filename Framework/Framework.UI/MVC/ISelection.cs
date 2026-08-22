using System.Collections.Generic;
using Framework.UI;

namespace Framework.MVC
{
    public interface ISelection<T> where T : ViewModel
    {
        IEnumerable<int> GetSelectedIds();
        void GetSelectedIds(IList<int> ids);
        int SingleId { get; }

        int SelCount { get; }

        T? Single { get; }
        IEnumerable<T> GetSelectedItems();
        void GetSelectedItems(IList<T> items);

        void SetSelectedId(int id);
        void SetSelectedIndex(int idx);
    }
}
