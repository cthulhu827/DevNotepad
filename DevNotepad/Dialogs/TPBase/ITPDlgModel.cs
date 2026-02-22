using DevNotepad.Core.TextTransformers;

namespace DevNotepad.Dialogs.TPBase;

public interface ITPDlgModel
{
    void Init(IParametrizedTextTransformer newTransformer, string[] newSource, ITransformerEditSession newEditSession);

    ITextTransformer Transformer { get; }
}