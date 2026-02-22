using Framework.UI;

namespace DevNotepad.Dialogs.TPTrimTextDlg
{
    public partial class v_TPTrimTextDlg : ModalDialog
    {
        public v_TPTrimTextDlg()
        {
            InitializeComponent();
        }

        private void v_TPTrimTextDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrBack;

            lblPrefix.ForeColor = UI.ClrListFore;
            lblPrefix.Font = UI.Font14;

            chkAutoPrefix.ForeColor = UI.ClrListFore;
            chkAutoPrefix.Font = UI.Font14;

            txtPrefix.Font = UI.Font14;

            lblSuffix.ForeColor = UI.ClrListFore;
            lblSuffix.Font = UI.Font14;

            chkAutoSuffix.ForeColor = UI.ClrListFore;
            chkAutoSuffix.Font = UI.Font14;

            txtSuffix.Font = UI.Font14;
        }
    }
}
