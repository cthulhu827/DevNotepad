namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Trim whitespace", "f9b379f7-5505-4736-bc31-58ae0e5ebc5a")]
    public class TrimWhitespaceTransformer : LineTransformer
    {
        protected override string TransformLine(string line)
        {
            return line.Trim();
        }
    }
}