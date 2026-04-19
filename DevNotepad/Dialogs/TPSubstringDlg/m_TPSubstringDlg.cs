using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPSubstringDlg;

public class m_TPSubstringDlg : m_TPBase<SubstringTransformer, p_TPSubstringDlg>
{
    public string Limit
    {
        get => Transformer.Limit;
        set => eventRaiser.Raise(() => Transformer.Limit = value, p_TPSubstringDlg.LimitChanged);
    }

    public SubstringType Type
    {
        get => Transformer.Type;
        set => eventRaiser.Raise(() =>
        {
            Transformer.Type = value;

            if (value != SubstringType.BySelection) return;

            var firstLine = source?.FirstOrDefault();
            if (!string.IsNullOrWhiteSpace(firstLine)) Limit = firstLine;
        }, p_TPSubstringDlg.TypeChanged);
    }

    public (int SelStart, int SelLength) Selection
    {
        get => Transformer.Selection;
        set => eventRaiser.Raise(() => Transformer.Selection = value, p_TPSubstringDlg.SelectionChanged);
    }
}