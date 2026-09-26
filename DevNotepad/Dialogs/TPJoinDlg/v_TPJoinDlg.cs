using Framework.UI;

namespace DevNotepad.Dialogs.TPJoinDlg
{
    public partial class v_TPJoinDlg : ModalDialog
    {
        public v_TPJoinDlg()
        {
            InitializeComponent();
        }

        private void v_TPJoinDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblSeparator.ForeColor = UI.ClrFont;
            lblSeparator.Font = UI.Font14;

            txtSeparator.BackColor = UI.ClrChatBg;
            txtSeparator.ForeColor = UI.ClrFont;
            txtSeparator.Font = UI.Font14;

            lblGroupSize.ForeColor = UI.ClrFont;
            lblGroupSize.Font = UI.Font14;

            txtGroupSize.BackColor = UI.ClrChatBg;
            txtGroupSize.ForeColor = UI.ClrFont;
            txtGroupSize.Font = UI.Font14;

            chkTemplate.ForeColor = UI.ClrFont;
            chkTemplate.Font = UI.Font14;
        }
    }
}
