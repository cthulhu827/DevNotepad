using Framework.UI;

namespace DevNotepad.Dialogs.TPEndWithSuffixDlg
{
    public partial class v_TPEndWithSuffixDlg : ModalDialog
    {
        public v_TPEndWithSuffixDlg()
        {
            InitializeComponent();
        }

        private void v_TPEndWithSuffixDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblSuffix.ForeColor = UI.ClrFont;

            txtSuffix.BackColor = UI.ClrChatBg;
            txtSuffix.ForeColor = UI.ClrFont;
            txtSuffix.Font = UI.Font14;

            chkExceptLastLine.ForeColor = UI.ClrFont;
            chkExceptLastLine.Font = UI.Font14;
        }
    }
}
