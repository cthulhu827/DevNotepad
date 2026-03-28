using DevNotepad.Controls.PipeControl;

namespace DevNotepad.Controls.MainWindow
{
    public partial class v_MainWindow : Form
    {
        public v_MainWindow()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData is (Keys.Control | Keys.Tab) or (Keys.Control | Keys.Shift | Keys.Tab))
            {
                OnKeyDown(new KeyEventArgs(keyData));
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void v_MainWindow_Load(object sender, EventArgs e)
        {
            tbPages.BackColor = PipeItemColors.ClrBack;
            tbPages.Font = UI.Font14;
        }
    }
}