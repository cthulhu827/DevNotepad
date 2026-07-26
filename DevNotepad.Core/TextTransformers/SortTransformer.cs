using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Sort lines", "dc77ffa8-688d-45f3-89bd-cb94ba541a2e")]
    public class SortTransformer : BaseTextTransformer
    {
        [TextTransformer("Sort lines descending", "ad1498c9-fbb3-4ad5-938e-edc7e84d1dd7")]
        private static ITextTransformer BuildSortDescending() => new SortTransformer { Descending = true };

        public bool Descending { get; set; }

        public override string[] Transform(string[] lines)
        {
            return Descending
                ? lines.OrderByDescending(s => s).ToArray()
                : lines.OrderBy(s => s).ToArray();
        }
    }
}