using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.MVC
{
    public interface IMultipleSelection
    {
        IEnumerable<int> GetSelectedIds();
        void GetSelectedIds(IList<int> ids);
        void SetSelectedIds(IEnumerable<int> ids);
        int SelCount { get; }
    }
}
