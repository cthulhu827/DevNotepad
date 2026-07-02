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

            lblLinesBefore.ForeColor = UI.ClrFont;
            lblLinesBefore.Font = UI.Font14;

            txtLinesBefore.BackColor = UI.ClrChatBg;
            txtLinesBefore.ForeColor = UI.ClrFont;
            txtLinesBefore.Font = UI.Font14;

            lblLinesAfter.ForeColor = UI.ClrFont;
            lblLinesAfter.Font = UI.Font14;

            txtLinesAfter.BackColor = UI.ClrChatBg;
            txtLinesAfter.ForeColor = UI.ClrFont;
            txtLinesAfter.Font = UI.Font14;
        }
    }
}

