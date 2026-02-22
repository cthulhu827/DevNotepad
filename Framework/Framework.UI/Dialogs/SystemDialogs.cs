using System;
using System.Windows.Forms;

namespace Framework.UI
{
    public static class SystemDialogs
    {
        public static void SaveFile(string filter, string fileName, Action<string> action)
        {
            using (var dialog = new SaveFileDialog())
            {
                dialog.Filter = filter;
                dialog.FileName = fileName;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    action(dialog.FileName);
                }
            }
        }
    }
}