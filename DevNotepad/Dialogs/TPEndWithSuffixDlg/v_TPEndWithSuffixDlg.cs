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
            BackColor = UI.ClrBack;

            lblSuffix.ForeColor = UI.ClrListFore;

            txtSuffix.BackColor = UI.ClrListBack;
            txtSuffix.ForeColor = UI.ClrListFore;
            txtSuffix.Font = UI.Font14;

            chkExceptLastLine.BackColor = UI.ClrBack;
            chkExceptLastLine.ForeColor = UI.ClrListFore;
            chkExceptLastLine.Font = UI.Font14;
        }
    }
}
