using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPEndWithSuffixDlg;

public class m_TPEndWithSuffixDlg : m_TPBase<EndWithSuffixTransformer, p_TPEndWithSuffixDlg>
{
    public string Suffix
    {
        get => Transformer.Suffix;
        set => eventRaiser.Raise(() => Transformer.Suffix = value, p_TPEndWithSuffixDlg.SuffixChanged);
    }

    public bool ExceptLastLine
    {
        get => Transformer.ExceptLastLine;
        set => eventRaiser.Raise(() => Transformer.ExceptLastLine = value, p_TPEndWithSuffixDlg.ExceptLastLineChanged);
    }
}