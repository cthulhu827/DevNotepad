namespace DevNotepad.Core.TextTransformers
{
    public interface ICombinedTextTransformer
    {
        ITextTransformer[] Components { get; }
    }
}