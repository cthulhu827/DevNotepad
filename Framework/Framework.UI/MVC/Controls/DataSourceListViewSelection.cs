using Framework.Domain;
using Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework.AppInfrastructure;

namespace Framework.UI
{
    public class DataSourceListViewSelection<T> : ISelection<T> where T : ViewModel
    {
        private readonly DataSourceListView<T> listView;

        public DataSourceListViewSelection(DataSourceListView<T> listView)
        {
            this.listView = listView;
        }

        public void GetSelectedIds(IList<int> ids)
        {
            ids.Clear();
            ids.AddRange(GetSelectedIds());
        }

        public IEnumerable<int> GetSelectedIds()
        {
            return GetSelectedItems().Select(item => item.Id);
        }

        public int SelCount
        {
            get { return listView.SelectedIndices.Count; }
        }

        public int SingleId
        {
            get
            {
                var single = Single;
                return single == null ? Entity.NullId : single.Id;
            }
        }

        public T Single
        {
            get
            {
                if (listView.SelectedIndices.Count != 1)
                {
                    return null;
                }

                var idx = listView.SelectedIndex;
                return idx == -1 ? null : listView.DataSource[idx];
            }
        }

        public IEnumerable<T> GetSelectedItems()
        {
            var result = new List<T>();
            GetSelectedItems(result);
            return result;
        }

        public void GetSelectedItems(IList<T> items)
        {
            items.Clear();
            foreach (int idx in listView.SelectedIndices)
            {
                items.Add(listView.DataSource[idx]);
            }
        }

        public void SetSelectedId(int id)
        {
            listView.DataSource.MessageBus.Notify_ListItemHighlight_ByID(id);
        }

        public void SetSelectedIndex(int idx)
        {
            listView.DataSource.MessageBus.Notify_ListItemHighlight_ByIndex(idx);
        }
    }
}
