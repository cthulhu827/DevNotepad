using System;
using System.Windows.Forms;
using System.Drawing;

namespace Framework.UI
{
    public partial class ModalDialog : Form
    {
        public ModalDialog()
        {
            InitializeComponent();
        }

        public Func<bool>? Validator;

        [Obsolete("Use DialogResultOk() method instead")]
        public new DialogResult DialogResult
        {
            get { return base.DialogResult; }
            set { base.DialogResult = value; }
        }

        public void DialogResultOk()
        {
            if (Validator == null || Validator())
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            btnCancel.Location = new Point(ClientSize.Width - 8 - btnCancel.Width, ClientSize.Height - 8 - btnCancel.Height);
            btnOK.Location = new Point(btnCancel.Left - 8 - btnOK.Width, ClientSize.Height - 8 - btnOK.Height);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResultOk();
        }
    }
}
