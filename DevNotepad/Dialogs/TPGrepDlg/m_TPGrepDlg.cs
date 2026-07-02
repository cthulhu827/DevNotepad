using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPGrepDlg;

public class m_TPGrepDlg : m_TPBase<GrepTransformer, p_TPGrepDlg>
{
    public string SearchText
    {
        get => Transformer.SearchText;
        set => eventRaiser.Raise(() => Transformer.SearchText = value, p_TPGrepDlg.SearchTextChanged);
    }

    public bool Exclude
    {
        get => Transformer.Exclude;
        set => eventRaiser.Raise(() => Transformer.Exclude = value, p_TPGrepDlg.ExcludeChanged);
    }

    public bool CaseSensitive
    {
        get => Transformer.CaseSensitive;
        set => eventRaiser.Raise(() => Transformer.CaseSensitive = value, p_TPGrepDlg.CaseSensitiveChanged);
    }

    public bool RegEx
    {
        get => Transformer.RegEx;
        set => eventRaiser.Raise(() => Transformer.RegEx = value, p_TPGrepDlg.RegExChanged);
    }

    public string LinesBefore
    {
        get => Transformer.LinesBefore.ToString();
        set => eventRaiser.Raise(
            () => Transformer.LinesBefore = int.TryParse(value, out var intValue) ? intValue : 0,
            p_TPGrepDlg.LinesBefore);
    }

    public string LinesAfter
    {
        get => Transformer.LinesAfter.ToString();
        set => eventRaiser.Raise(
            () => Transformer.LinesAfter = int.TryParse(value, out var intValue) ? intValue : 0,
            p_TPGrepDlg.LinesAfter);
    }
}