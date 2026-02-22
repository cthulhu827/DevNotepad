using Framework.AppInfrastructure;
using Framework.MVC;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace Framework.UI;

public class DataSourceListBox<T> : ListBox where T : ViewModel
{
    private IDataSource<T> dataSource = DataSourceFactory.CreateNull<T>();

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new IDataSource<T>? DataSource
    {
        get => dataSource;
        set
        {
            (dataSource as IDomainListener)?.StopListeningDomain();

            StopListeningDataSource();
            ClearUI();

            dataSource = value ?? DataSourceFactory.CreateNull<T>();

            UpdateUI();
            StartListeningDataSource();

            (dataSource as IDomainListener)?.StartListeningDomain();
        }
    }

    private ISelection<T>? selection;

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ISelection<T> Selection => selection ??= DoInitSelection();

    protected virtual ISelection<T> DoInitSelection()
    {
        return new DataSourceListBoxSelection<T>(this);
    }

    private void On_NEListChanged(NEListChanged evnt)
    {
        UpdateUI();
    }

    private void On_NEListChangedContents(NEListChangedContents evnt)
    {
        if (evnt.ItemIndex == SelectedIndex)
        {
            Invalidate();
        }
    }

    private void On_NEListClear(NEListClear evnt)
    {
        ClearUI();
    }

    private void On_NEListItemHighlight(NEListItemHighlight evnt)
    {
        switch (evnt.HighlightType)
        {
            case NEListItemHighlight.HighlightId:
                SelectedItem = dataSource.Items.FirstOrDefault(item => item.Id == evnt.Item);
                break;
            case NEListItemHighlight.HighlightIndex:
                SelectedIndex = evnt.Item;
                break;
            default:
                break;
        }
    }

    private void StartListeningDataSource()
    {
        dataSource.MessageBus.Sign<NEListChanged>(On_NEListChanged);
        dataSource.MessageBus.Sign<NEListChangedContents>(On_NEListChangedContents);
        dataSource.MessageBus.Sign<NEListClear>(On_NEListClear);
        dataSource.MessageBus.Sign<NEListItemHighlight>(On_NEListItemHighlight);
    }

    private void UpdateUI()
    {
        Items.Clear();
        Items.AddRange(dataSource.Items.ToArray());
    }

    private void ClearUI()
    {
        Items.Clear();
    }

    private void StopListeningDataSource()
    {
        dataSource.MessageBus.UnsignObject(this);
    }
}