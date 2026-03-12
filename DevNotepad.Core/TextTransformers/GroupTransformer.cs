using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Group", "c3c4cbb9-4608-41e1-9e1f-8230ec4d61e2")]
    public class GroupTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            return lines
                .GroupBy(line => line)
                .OrderByDescending(g => g.Count())
                .Select(g => $"{g.Key} = {g.Count()}")
                .ToArray();
        }
    }
}