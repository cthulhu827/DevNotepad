using Framework.MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.UI
{
    class DataSourceCheckedListBoxSelection<T> : IMultipleSelection where T : ViewModel
    {
        private readonly DataSourceCheckedListBox<T> checkedListBox;

        public DataSourceCheckedListBoxSelection(DataSourceCheckedListBox<T> checkedListBox)
        {
            this.checkedListBox = checkedListBox;
        }

        public IEnumerable<int> GetSelectedIds()
        {
            var result = new List<int>();
            GetSelectedIds(result);
            return result;
        }

        public void GetSelectedIds(IList<int> ids)
        {
            ids.Clear();
            foreach (int idx in checkedListBox.CheckedIndices)
                ids.Add(checkedListBox.DataSource[idx].Id);
        }

        public void SetSelectedIds(IEnumerable<int> ids)
        {
            for (int i = 0; i < checkedListBox.DataSource.Count; i++)
            {
                checkedListBox.SetItemChecked(i, ids.Contains(checkedListBox.DataSource[i].Id));
            }
        }

        public int SelCount
        {
            get { return checkedListBox.CheckedIndices.Count; }
        }
    }
}
