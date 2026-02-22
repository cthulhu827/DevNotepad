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
            BackColor = UI.ClrBack;

            txtParameterText.BackColor = UI.ClrListBack;
            txtParameterText.ForeColor = UI.ClrListFore;
            txtParameterText.Font = UI.Font14;
        }
    }
}
