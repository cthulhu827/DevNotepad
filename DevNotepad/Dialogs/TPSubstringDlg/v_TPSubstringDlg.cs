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

            rbSkipStart.ForeColor = UI.ClrFont;
            rbSkipStart.Font = UI.Font14;

            rbSkipEnd.ForeColor = UI.ClrFont;
            rbSkipEnd.Font = UI.Font14;

            rbTakeStart.ForeColor = UI.ClrFont;
            rbTakeStart.Font = UI.Font14;

            rbTakeEnd.ForeColor = UI.ClrFont;
            rbTakeEnd.Font = UI.Font14;

            rbTakeBefore.ForeColor = UI.ClrFont;
            rbTakeBefore.Font = UI.Font14;

            rbTakeAfter.ForeColor = UI.ClrFont;
            rbTakeAfter.Font = UI.Font14;
        }
    }
}
