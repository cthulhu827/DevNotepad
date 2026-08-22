using DevNotepad.Dialogs.TPSingleParamDlg;
using Framework.MVC;
using System.Security.Cryptography.Xml;

namespace DevNotepad.Dialogs.InputBox;

public class m_InputBox : MVC_Model
{
    private string text = "";

    private readonly EventRaiser<p_InputBox> eventRaiser;

    public m_InputBox(string caption)
    {
        Caption = caption;
        eventRaiser = new EventRaiser<p_InputBox>(changes => NotifyChanged(changes.Select(change => Convert.ToInt32(change)).ToArray()));
    }

    public string Caption { get; }

    public string Text
    {
        get => text;
        set => eventRaiser.Raise(() => text = value, p_InputBox.TextChanged);
    }
}