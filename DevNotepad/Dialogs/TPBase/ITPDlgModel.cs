using DevNotepad.Core.TextTransformers;

namespace DevNotepad.Dialogs.TPBase;

public interface ITPDlgModel
{
    void Init(IParametrizedTextTransformer newTransformer, string[] newSource, TransformerEditContext newContext);

    ITextTransformer Transformer { get; }
}