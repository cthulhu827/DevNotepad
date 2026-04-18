namespace DevNotepad.Core.TextTransformers
{
    public interface ISingleParameterTextTransformer
    {
        string Parameter { get; set; }

        string Hint => string.Empty;
    }
}