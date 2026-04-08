using Framework.UI;

namespace DevNotepad.Dialogs.TransformersDlg
{
    public partial class v_TransformersDlg : ModalDialog
    {
        public v_TransformersDlg()
        {
            InitializeComponent();
        }

        public void SelectNextItem()
        {
            lbTransformers.SelectedIndex = lbTransformers.Items.Count == 0
                ? -1
                : Math.Min(lbTransformers.SelectedIndex + 1, lbTransformers.Items.Count - 1);
        }

        public void SelectPrevItem()
        {
            lbTransformers.SelectedIndex = lbTransformers.Items.Count == 0
                ? -1
                : Math.Max(lbTransformers.SelectedIndex - 1, 0);
        }

        private void v_TransformersDlg_Load(object sender, EventArgs e)
        {
            BackColor = UI.ClrChatListBg;

            txtSearch.BackColor = UI.ClrChatBg;
            txtSearch.ForeColor = UI.ClrFont;
            txtSearch.Font = UI.Font14;

            lbTransformers.BackColor = UI.ClrChatListBg;
            lbTransformers.Font = UI.Font14;
        }
    }
}