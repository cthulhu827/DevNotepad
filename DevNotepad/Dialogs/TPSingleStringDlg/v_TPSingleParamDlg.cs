using Framework.UI;

namespace DevNotepad.Dialogs.TPSingleParamDlg
{
    public partial class v_TPSingleParamDlg : ModalDialog
    {
        public v_TPSingleParamDlg()
        {
            InitializeComponent();
        }

        private void v_TPSingleParamDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            txtParameterText.BackColor = UI.ClrChatBg;
            txtParameterText.ForeColor = UI.ClrFont;
            txtParameterText.Font = UI.Font14;
        }
    }
}
