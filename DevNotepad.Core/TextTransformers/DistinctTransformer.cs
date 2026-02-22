using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Distinct", "79b35de9-c478-47f9-8e67-0681e02f0adf")]
    public class DistinctTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            return lines.Distinct().ToArray();
        }
    }
}