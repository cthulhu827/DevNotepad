using System;
using Framework.Domain;
using Framework.MVC;
using System.Collections.Generic;
using System.Linq;
using Framework.AppInfrastructure;

namespace Framework.UI;

public class DataSourceListBoxSelection<T> : ISelection<T> where T : ViewModel
{
    private readonly DataSourceListBox<T> listBox;

    public DataSourceListBoxSelection(DataSourceListBox<T> listBox)
    {
        this.listBox = listBox;
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

    public int SelCount => (Single != null).ToInt();

    public int SingleId
    {
        get
        {
            var single = Single;
            return single?.Id ?? Entity.NullId;
        }
    }

    public T? Single => (T)listBox.SelectedItem;

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
        listBox.DataSource?.MessageBus.Notify_ListItemHighlight_ByID(id);
    }

    public void SetSelectedIndex(int idx)
    {
        listBox.DataSource?.MessageBus.Notify_ListItemHighlight_ByIndex(idx);
    }
}