using DevNotepad.Controls.PipeControl;

namespace DevNotepad.Controls.MainWindow
{
    public partial class v_MainWindow : Form
    {
        public v_MainWindow()
        {
            InitializeComponent();
        }

        private void v_MainWindow_Load(object sender, EventArgs e)
        {
            tbPages.BackColor = PipeItemColors.ClrBack;
            tbPages.Font = UI.Font14;
        }
    }
}