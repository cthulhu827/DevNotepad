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
            BackColor = UI.ClrChatListBg;

            lblTotalLength.ForeColor = UI.ClrFont;
            lblTotalLength.Font = UI.Font14;

            txtTotalLength.BackColor = UI.ClrChatBg;
            txtTotalLength.ForeColor = UI.ClrFont;
            txtTotalLength.Font = UI.Font14;

            lblSymbol.ForeColor = UI.ClrFont;
            lblSymbol.Font = UI.Font14;

            txtSymbol.BackColor = UI.ClrChatBg;
            txtSymbol.ForeColor = UI.ClrFont;
            txtSymbol.Font = UI.Font14;

            lblType.ForeColor = UI.ClrFont;
            lblType.Font = UI.Font14;

            rbLeading.ForeColor = UI.ClrFont;
            rbLeading.Font = UI.Font14;

            rbTrailing.ForeColor = UI.ClrFont;
            rbTrailing.Font = UI.Font14;
        }
    }
}
