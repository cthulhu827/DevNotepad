using System;
using System.Windows.Forms;

namespace Framework.UI
{
    public partial class InputBox : ModalDialog
    {
        public InputBox()
        {
            InitializeComponent();
        }

        private bool allowEmptyRes = false;

        private bool DoShow(string caption, string prompt, ref string text, bool allowEmptyRes)
        {
            this.allowEmptyRes = allowEmptyRes;
            Text = caption;
            lblPrompt.Text = prompt.EndsWith(":") ? prompt : prompt + ":";
            txtValue.Text = text;

            bool result = ShowDialog() == DialogResult.OK;
            if (result) text = txtValue.Text;

            return result;
        }

        private void txtValue_TextChanged(object sender, EventArgs e)
        {
            btnOK.Enabled = allowEmptyRes ? true : (!string.IsNullOrEmpty(txtValue.Text));
        }

        private void TfrmInputBoxDialog_Shown(object sender, EventArgs e)
        {
            txtValue.Focus();
        }

        public static bool Show(string caption, string prompt, ref string text, bool allowEmptyRes)
        {
            InputBox dlg = new InputBox();
            return dlg.DoShow(caption, prompt, ref text, allowEmptyRes);
        }
    }
}
