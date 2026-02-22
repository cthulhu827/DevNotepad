using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Remove empty lines", "00aa3764-f4c3-4a78-99e7-70e30d26c15f")]
    public class RemoveEmptyLinesTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            return lines.Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
        }
    }
}