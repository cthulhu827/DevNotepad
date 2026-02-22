using DevNotepad.Controls.MainWindow;

namespace DevNotepad
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var cMainWindow = new c_MainWindow
            {
                View = new v_MainWindow(),
                Model = new m_MainWindow()
            };

            Application.Run(cMainWindow.View);
        }
    }
}