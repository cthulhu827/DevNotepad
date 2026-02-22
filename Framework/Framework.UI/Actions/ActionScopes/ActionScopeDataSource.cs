using Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    public class ActionScopeDataSource<T> : ActionScope, IAS_DataSource<T> where T : ViewModel
    {
        private readonly Func<ISelection<T>> getSelectionDelegate;

        private readonly Func<IDataSource<T>> getDataSourceDelegate;

        public ISelection<T> Selection
        {
            get { return getSelectionDelegate == null ? null : getSelectionDelegate(); }
        }

        public IDataSource<T> DataSource
        {
            get { return getDataSourceDelegate == null ? null : getDataSourceDelegate(); }
        }

        public ActionScopeDataSource(Func<IDataSource<T>> getDataSourceDelegate, Func<ISelection<T>> getSelectionDelegate)
        {
            this.getDataSourceDelegate = getDataSourceDelegate;
            this.getSelectionDelegate = getSelectionDelegate;
        }
    }
}
