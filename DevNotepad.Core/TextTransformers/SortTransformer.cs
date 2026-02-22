using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Sort lines", "dc77ffa8-688d-45f3-89bd-cb94ba541a2e")]
    public class SortTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            return lines.OrderBy(s => s).ToArray();
        }
    }
}