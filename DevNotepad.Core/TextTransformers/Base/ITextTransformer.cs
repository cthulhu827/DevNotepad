namespace DevNotepad.Core.TextTransformers
{
    public interface ITextTransformer
    {
        string Caption { get; set; }
        string[] Transform(string[] lines);
    }
}