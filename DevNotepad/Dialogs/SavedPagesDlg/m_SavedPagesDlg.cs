using DevNotepad.Dialogs.SearchListDlg;

namespace DevNotepad.Dialogs.SavedPagesDlg;

public class m_SavedPagesDlg : m_SearchListDlg<VM_SavedPage>
{
    public m_SavedPagesDlg()
        : base(SavedPages.SavedPages.GetPages())
    {
    }
}