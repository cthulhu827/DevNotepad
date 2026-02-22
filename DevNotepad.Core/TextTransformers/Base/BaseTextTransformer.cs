namespace DevNotepad.Core.TextTransformers
{
    public abstract class BaseTextTransformer : ITextTransformer
    {
        public string Caption { get; set; } = "";

        public abstract string[] Transform(string[] lines);
    }
}