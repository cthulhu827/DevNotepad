using Framework.UI;

namespace DevNotepad.Controls.ToolBar;

public class VM_ToolButton : ViewModel
{
    private static int GlobalId = 1;

    public VM_ToolButton(string text)
    {
        Id = GlobalId++;
        Text = text;
    }
}
