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
            BackColor = UI.ClrBack;

            lblLimit.ForeColor = UI.ClrListFore;
            lblLimit.Font = UI.Font14;

            txtLimit.BackColor = UI.ClrListBack;
            txtLimit.ForeColor = UI.ClrListFore;
            txtLimit.Font = UI.Font14;

            lblType.ForeColor = UI.ClrListFore;
            lblType.Font = UI.Font14;

            rbSkipStart.ForeColor = UI.ClrListFore;
            rbSkipStart.Font = UI.Font14;

            rbSkipEnd.ForeColor = UI.ClrListFore;
            rbSkipEnd.Font = UI.Font14;

            rbTakeStart.ForeColor = UI.ClrListFore;
            rbTakeStart.Font = UI.Font14;

            rbTakeEnd.ForeColor = UI.ClrListFore;
            rbTakeEnd.Font = UI.Font14;

            rbTakeBefore.ForeColor = UI.ClrListFore;
            rbTakeBefore.Font = UI.Font14;

            rbTakeAfter.ForeColor = UI.ClrListFore;
            rbTakeAfter.Font = UI.Font14;
        }
    }
}
