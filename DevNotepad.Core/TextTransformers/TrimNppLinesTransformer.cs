using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Trim Notepad++ / NPP lines", "43c4d963-4a38-4c3a-bfdc-eb68d60fc613")]
    public class TrimNppLinesTransformer : PipeTransformer
    {
        protected override IEnumerable<ITextTransformer> Init()
        {
            // todo: лучше через RegEx
            yield return new GrepTransformer("Line ");
            yield return new SubstringTransformer(":", SubstringType.TakeAfter);
            yield return new TrimWhitespaceTransformer();
        }
    }
}