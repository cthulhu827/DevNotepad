using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Join pairs", "dca20309-d398-4279-a842-2af8daa72fa4")]
    public class JoinPairsTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            if (lines.Length < 2) return lines;

            var result = new List<string>();
            for (int i = 0; i < lines.Length; i += 2)
            {
                var s = lines[i];
                if (i + 1 < lines.Length) s += " / " + lines[i + 1];
                result.Add(s);
            }

            return result.ToArray();
        }
    }
}