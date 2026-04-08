using Framework.UI;

namespace DevNotepad.Dialogs.TPGrepDlg
{
    public partial class v_TPGrepDlg : ModalDialog
    {
        public v_TPGrepDlg()
        {
            InitializeComponent();
        }

        private void v_TPGrepDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblSearchText.ForeColor = UI.ClrFont;
            lblSearchText.Font = UI.Font14;

            txtSearchText.BackColor = UI.ClrChatBg;
            txtSearchText.ForeColor = UI.ClrFont;
            txtSearchText.Font = UI.Font14;

            chkExclude.ForeColor = UI.ClrFont;
            chkExclude.Font = UI.Font14;

            chkCaseSensitive.ForeColor = UI.ClrFont;
            chkCaseSensitive.Font = UI.Font14;

            chkRegEx.ForeColor = UI.ClrFont;
            chkRegEx.Font = UI.Font14;
        }
    }
}

