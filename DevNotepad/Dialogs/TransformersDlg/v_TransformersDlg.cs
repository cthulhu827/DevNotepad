using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            BackColor = UI.ClrBack;

            txtSearch.BackColor = UI.ClrListBack;
            txtSearch.ForeColor = UI.ClrListFore;
            txtSearch.Font = UI.Font14;

            lbTransformers.BackColor = UI.ClrListBack;
            lbTransformers.Font = new Font(lbTransformers.Font.FontFamily, 14);
        }
    }
}