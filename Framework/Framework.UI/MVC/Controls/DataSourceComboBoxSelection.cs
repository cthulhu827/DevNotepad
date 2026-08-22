using Framework.Domain;
using Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using Framework.AppInfrastructure;

namespace Framework.UI
{
    public class DataSourceComboBoxSelection<T> : ISelection<T> where T : ViewModel
    {
        private readonly DataSourceComboBox<T> comboBox;

        public DataSourceComboBoxSelection(DataSourceComboBox<T> comboBox)
        {
            this.comboBox = comboBox;
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
            get { return (Single != null).ToInt(); }
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
            get { return (T)comboBox.SelectedItem; }
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
            var single = Single;
            if (single != null)
            {
                items.Add(single);
            }
        }

        public void SetSelectedId(int id)
        {
            comboBox.DataSource.MessageBus.Notify_ListItemHighlight_ByID(id);
        }

        public void SetSelectedIndex(int idx)
        {
            comboBox.DataSource.MessageBus.Notify_ListItemHighlight_ByIndex(idx);
        }
    }
}
