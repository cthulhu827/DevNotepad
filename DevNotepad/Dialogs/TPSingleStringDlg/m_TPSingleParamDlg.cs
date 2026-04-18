using DevNotepad.Core.TextTransformers;
using DevNotepad.Dialogs.TPBase;

namespace DevNotepad.Dialogs.TPSingleParamDlg;

public class m_TPSingleParamDlg<TTransformer> : m_TPBase<TTransformer, p_TPSingleParamDlg>, ISingleParameterTextTransformer
    where TTransformer : class, ITextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
{
    public string Parameter
    {
        get => Transformer.Parameter;
        set => eventRaiser.Raise(() => Transformer.Parameter = value, p_TPSingleParamDlg.ParameterChanged);
    }

    public string Hint => Transformer.Hint;
}