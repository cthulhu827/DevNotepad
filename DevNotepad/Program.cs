using DevNotepad.Controls.MainWindow;
using DevNotepad.Core;
using DevNotepad.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace DevNotepad
{
    internal static class Program
    {
        public static AppSettings Settings { get; private set; } = new AppSettings();

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();
            Settings = configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

            Domain.ShortcutConverter = new KeysShortcutConverter();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var model = new m_MainWindow();
            model.AddPage();
            var cMainWindow = new c_MainWindow
            {
                View = new v_MainWindow(),
                Model = model
            };

            Application.Run(cMainWindow.View);
        }
    }
}