using DevNotepad.Dialogs.SearchListDlg;
using Framework.UI;

namespace DevNotepad.Dialogs.SavedPagesDlg
{
    public partial class v_SavedPagesDlg : ModalDialog, ISearchListDlg<VM_SavedPage>
    {
        public v_SavedPagesDlg()
        {
            InitializeComponent();
        }

        private void v_SavedPagesDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            txtSearch.BackColor = UI.ClrChatBg;
            txtSearch.ForeColor = UI.ClrFont;
            txtSearch.Font = UI.Font14;

            lbSavedPages.BackColor = UI.ClrChatListBg;
            lbSavedPages.Font = UI.Font14;
        }

        TextBox ISearchListDlg<VM_SavedPage>.SearchTextBox => txtSearch;

        DataSourceListBox<VM_SavedPage> ISearchListDlg<VM_SavedPage>.ListBox => lbSavedPages;
    }
}