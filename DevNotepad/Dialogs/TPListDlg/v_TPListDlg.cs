using Framework.UI;

namespace DevNotepad.Dialogs.TPListDlg
{
    public partial class v_TPListDlg : ModalDialog
    {
        public v_TPListDlg()
        {
            InitializeComponent();
        }

        private void v_TPListDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblList1.ForeColor = UI.ClrFont;
            lblList1.Font = UI.Font14;

            lblList2.ForeColor = UI.ClrFont;
            lblList2.Font = UI.Font14;

            lbList1.BackColor = UI.ClrChatBg;
            lbList1.ForeColor = UI.ClrFont;
            lbList1.Font = UI.Font14;

            lbList2.BackColor = UI.ClrChatBg;
            lbList2.ForeColor = UI.ClrFont;
            lbList2.Font = UI.Font14;

            lblOperation.ForeColor = UI.ClrFont;
            lblOperation.Font = UI.Font14;

            rbExclude.ForeColor = UI.ClrFont;
            rbExclude.Font = UI.Font14;

            rbIntersect.ForeColor = UI.ClrFont;
            rbIntersect.Font = UI.Font14;

            rbUnion.ForeColor = UI.ClrFont;
            rbUnion.Font = UI.Font14;

            rbConcat.ForeColor = UI.ClrFont;
            rbConcat.Font = UI.Font14;
        }
    }
}