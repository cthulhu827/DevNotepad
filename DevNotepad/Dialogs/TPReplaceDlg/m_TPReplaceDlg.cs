using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPReplaceDlg;

public class m_TPReplaceDlg : m_TPBase<ReplaceTransformer, p_TPReplaceDlg>
{
    public string OldValue
    {
        get => Transformer.OldValue;
        set => eventRaiser.Raise(() => Transformer.OldValue = value, p_TPReplaceDlg.OldValueChanged);
    }

    public string NewValue
    {
        get => Transformer.NewValue;
        set => eventRaiser.Raise(() => Transformer.NewValue = value, p_TPReplaceDlg.NewValueChanged);
    }
}
