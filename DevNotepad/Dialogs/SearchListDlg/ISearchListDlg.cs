using Framework.UI;

namespace DevNotepad.Dialogs.SearchListDlg;

public interface ISearchListDlg<TViewModel>
    where TViewModel : ViewModel
{
    TextBox SearchTextBox { get; }
    DataSourceListBox<TViewModel> ListBox { get; }
}