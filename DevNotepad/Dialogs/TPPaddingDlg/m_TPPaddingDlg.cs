using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPPaddingDlg;

public class m_TPPaddingDlg : m_TPBase<PaddingTransformer, p_TPPaddingDlg>
{
    public string TotalLength
    {
        get => Transformer.TotalLength;
        set => eventRaiser.Raise(() => Transformer.TotalLength = value, p_TPPaddingDlg.TotalLengthChanged);
    }

    public string Symbol
    {
        get => Transformer.Symbol == '\0' ? string.Empty : Transformer.Symbol.ToString();
        set
        {
            char ch = value.Length > 0 ? value[0] : '\0';
            eventRaiser.Raise(() => Transformer.Symbol = ch, p_TPPaddingDlg.SymbolChanged);
        }
    }

    public PaddingType PaddingType
    {
        get => Transformer.PaddingType;
        set => eventRaiser.Raise(() => Transformer.PaddingType = value, p_TPPaddingDlg.PaddingTypeChanged);
    }
}
