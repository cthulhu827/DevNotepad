using Framework.UI;

namespace DevNotepad.Dialogs.TPReplaceDlg
{
    public partial class v_TPReplaceDlg : ModalDialog
    {
        public v_TPReplaceDlg()
        {
            InitializeComponent();
        }

        private void v_TPReplaceDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            lblOldValue.ForeColor = UI.ClrFont;
            lblOldValue.Font = UI.Font14;

            txtOldValue.BackColor = UI.ClrChatBg;
            txtOldValue.ForeColor = UI.ClrFont;
            txtOldValue.Font = UI.Font14;

            lblNewValue.ForeColor = UI.ClrFont;
            lblNewValue.Font = UI.Font14;

            txtNewValue.BackColor = UI.ClrChatBg;
            txtNewValue.ForeColor = UI.ClrFont;
            txtNewValue.Font = UI.Font14;
        }
    }
}
