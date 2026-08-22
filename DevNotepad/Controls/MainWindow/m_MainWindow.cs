using DevNotepad.Controls.Page;
using DevNotepad.Controls.ToolBar;
using Framework.MVC;

namespace DevNotepad.Controls.MainWindow;

public class m_MainWindow : MVC_Model
{
    private readonly EventRaiser<p_MainWindow> eventRaiser;

    private readonly IList<m_Page> pages = new List<m_Page>();

    private int selectedIndex = -1;

    public m_MainWindow()
    {
        eventRaiser = new EventRaiser<p_MainWindow>(ApplyChanges);
    }

    public m_Page[] Pages => pages.ToArray();

    public IDataSource<VM_ToolButton> ToolButtons { get; } = DataSourceFactory.Create<VM_ToolButton>();

    public int SelectedIndex
    {
        get => selectedIndex;
        set => eventRaiser.Raise(() => selectedIndex = value, p_MainWindow.SelectedIndexChanged);
    }

    public void AddPage(m_Page? model = null)
    {
        eventRaiser.Raise(() =>
        {
            model ??= new m_Page(true, $"Page {pages.Count + 1}");
            pages.Add(model);
            ToolButtons.AddItem(new VM_ToolButton(model.Caption));
            SelectedIndex = pages.Count - 1;
        }, p_MainWindow.PageAdded);
    }

    public void NextPage(bool forward)
    {
        int offset = forward ? 1 : -1;
        var newIdx = selectedIndex + offset;
        if (newIdx < 0)
            newIdx = pages.Count - 1;
        else if (newIdx > pages.Count - 1)
            newIdx = 0;
        if (newIdx != selectedIndex) SelectedIndex = newIdx;
    }

    private void ApplyChanges(ICollection<p_MainWindow> changes)
    {
        NotifyChanged(changes.Select(change => (int)change).ToArray());
    }
}