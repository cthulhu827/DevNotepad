using Framework.UI;

namespace DevNotepad.Dialogs.TPGrepDlg
{
    public partial class v_TPGrepDlg : ModalDialog
    {
        public v_TPGrepDlg()
        {
            InitializeComponent();
        }

        private void v_TPGrepDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrBack;

            lblSearchText.ForeColor = UI.ClrListFore;
            lblSearchText.Font = UI.Font14;

            txtSearchText.BackColor = UI.ClrListBack;
            txtSearchText.ForeColor = UI.ClrListFore;
            txtSearchText.Font = UI.Font14;

            chkExclude.ForeColor = UI.ClrListFore;
            chkExclude.Font = UI.Font14;

            chkCaseSensitive.ForeColor = UI.ClrListFore;
            chkCaseSensitive.Font = UI.Font14;

            chkRegEx.ForeColor = UI.ClrListFore;
            chkRegEx.Font = UI.Font14;
        }
    }
}

