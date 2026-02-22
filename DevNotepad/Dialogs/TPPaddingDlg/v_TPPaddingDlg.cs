using Framework.UI;

namespace DevNotepad.Dialogs.TPPaddingDlg
{
    public partial class v_TPPaddingDlg : ModalDialog
    {
        public v_TPPaddingDlg()
        {
            InitializeComponent();
        }

        private void v_TPPaddingDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrBack;

            lblTotalLength.ForeColor = UI.ClrListFore;
            lblTotalLength.Font = UI.Font14;

            txtTotalLength.BackColor = UI.ClrListBack;
            txtTotalLength.ForeColor = UI.ClrListFore;
            txtTotalLength.Font = UI.Font14;

            lblSymbol.ForeColor = UI.ClrListFore;
            lblSymbol.Font = UI.Font14;

            txtSymbol.BackColor = UI.ClrListBack;
            txtSymbol.ForeColor = UI.ClrListFore;
            txtSymbol.Font = UI.Font14;

            lblType.ForeColor = UI.ClrListFore;
            lblType.Font = UI.Font14;

            rbLeading.ForeColor = UI.ClrListFore;
            rbLeading.Font = UI.Font14;

            rbTrailing.ForeColor = UI.ClrListFore;
            rbTrailing.Font = UI.Font14;
        }
    }
}
