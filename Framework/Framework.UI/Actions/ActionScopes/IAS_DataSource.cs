using Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public interface IAS_DataSource<T> : IAS_Base where T : ViewModel
    {
        ISelection<T> Selection { get; }
        IDataSource<T> DataSource { get; }
    }
}
