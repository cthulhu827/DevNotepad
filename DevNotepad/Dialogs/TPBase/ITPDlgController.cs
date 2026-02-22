using DevNotepad.Core.TextTransformers;

namespace DevNotepad.Dialogs.TPBase;

public interface ITPDlgController
{
    bool Edit(IParametrizedTextTransformer transformer, string[] source, ITransformerEditSession editSession);
}