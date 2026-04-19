using Framework.UI;

namespace DevNotepad.Dialogs.TPSubstringDlg
{
    public partial class v_TPSubstringDlg : ModalDialog
    {
        public v_TPSubstringDlg()
        {
            InitializeComponent();
        }

        private void v_TPSubstringDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblLimit.ForeColor = UI.ClrFont;
            lblLimit.Font = UI.Font14;

            txtLimit.BackColor = UI.ClrChatBg;
            txtLimit.ForeColor = UI.ClrFont;
            txtLimit.Font = UI.Font14;

            lblType.ForeColor = UI.ClrFont;
            lblType.Font = UI.Font14;
        }
    }
}
