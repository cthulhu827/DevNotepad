using DevNotepad.Dialogs.SearchListDlg;
using Framework.UI;

namespace DevNotepad.Dialogs.TransformersDlg
{
    public partial class v_TransformersDlg : ModalDialog, ISearchListDlg<VM_TransformerForDlg>
    {
        public v_TransformersDlg()
        {
            InitializeComponent();
        }

        private void v_TransformersDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            txtSearch.BackColor = UI.ClrChatBg;
            txtSearch.ForeColor = UI.ClrFont;
            txtSearch.Font = UI.Font14;

            lbTransformers.BackColor = UI.ClrChatListBg;
            lbTransformers.Font = UI.Font14;
        }

        TextBox ISearchListDlg<VM_TransformerForDlg>.SearchTextBox => txtSearch;

        DataSourceListBox<VM_TransformerForDlg> ISearchListDlg<VM_TransformerForDlg>.ListBox => lbTransformers;
    }
}