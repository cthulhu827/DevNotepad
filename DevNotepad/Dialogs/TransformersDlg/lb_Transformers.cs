using Framework.AppInfrastructure;
using Framework.UI;

namespace DevNotepad.Dialogs.TransformersDlg;

public class lb_Transformers : DataSourceListBox<VM_TransformerForDlg>
{
    // todo: перенести в ISelection
    public void SetSelectedId(int id)
    {
        DataSource.MessageBus.Notify_ListItemHighlight_ByID(id);
    }

    // todo: перенести в ISelection
    public void SetSelectedIndex(int idx)
    {
        DataSource.MessageBus.Notify_ListItemHighlight_ByIndex(idx);
    }
}