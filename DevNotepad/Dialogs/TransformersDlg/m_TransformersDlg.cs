using DevNotepad.Dialogs.SearchListDlg;

namespace DevNotepad.Dialogs.TransformersDlg;

public class m_TransformersDlg : m_SearchListDlg<VM_TransformerForDlg>
{
    public m_TransformersDlg(VM_TransformerForDlg[] all)
        : base(all)
    {
    }
}