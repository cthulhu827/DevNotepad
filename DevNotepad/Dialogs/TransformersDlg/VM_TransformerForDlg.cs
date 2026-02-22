using Framework.UI;

namespace DevNotepad.Dialogs.TransformersDlg;

public class VM_TransformerForDlg : ViewModel
{
    private static int GlobalId = 1;

    public VM_TransformerForDlg(Guid transformerId, string caption)
    {
        TransformerId = transformerId;
        Id = GlobalId++;
        Text = caption;
    }

    public Guid TransformerId { get; }
}