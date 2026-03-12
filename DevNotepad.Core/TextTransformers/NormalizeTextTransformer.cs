using System;
using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Normalize lines", "c6f5e323-c8f4-4e9a-936b-dd056315ac76")]
    public class NormalizeTextTransformer : PipeTransformer
    {
        protected override IEnumerable<ITextTransformer> Init()
        {
            yield return new RemoveEmptyLinesTransformer();
            yield return new TrimWhitespaceTransformer();
        }
    }
}