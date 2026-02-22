using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
