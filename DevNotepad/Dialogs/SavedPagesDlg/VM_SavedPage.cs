using Framework.UI;

namespace DevNotepad.Dialogs.SavedPagesDlg;

public class VM_SavedPage : ViewModel
{
    private static int GlobalId = 1;

    public VM_SavedPage(string caption, string fileName)
    {
        Id = GlobalId++;
        Text = caption;
        FileName = fileName;
    }

    public string FileName { get; }
}