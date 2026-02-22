using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPTrimTextDlg;

public class m_TPTrimTextDlg : m_TPBase<TrimTextTransformer, p_TPTrimTextDlg>
{
    public bool AutoPrefix
    {
        get => Transformer.Prefix == null;
        set => eventRaiser.Raise(() => Transformer.Prefix = value ? null : Transformer.CalculatedPrefix,
            p_TPTrimTextDlg.AutoPrefixChanged, p_TPTrimTextDlg.PrefixChanged);
    }

    public string Prefix
    {
        get => Transformer.CalculatedPrefix!;
        set => eventRaiser.Raise(() => Transformer.Prefix = value, p_TPTrimTextDlg.PrefixChanged);
    }

    public bool AutoSuffix
    {
        get => Transformer.Suffix == null;
        set => eventRaiser.Raise(() => Transformer.Suffix = value ? null : Transformer.CalculatedSuffix,
            p_TPTrimTextDlg.AutoSuffixChanged, p_TPTrimTextDlg.SuffixChanged);
    }

    public string Suffix
    {
        get => Transformer.CalculatedSuffix!;
        set => eventRaiser.Raise(() => Transformer.Suffix = value, p_TPTrimTextDlg.SuffixChanged);
    }
}