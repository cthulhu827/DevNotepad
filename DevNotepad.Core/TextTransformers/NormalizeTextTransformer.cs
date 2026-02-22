using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Normalize lines", "c6f5e323-c8f4-4e9a-936b-dd056315ac76")]
    public class NormalizeTextTransformer : PipeTransformer
    {
        public NormalizeTextTransformer()
            : base(
                new RemoveEmptyLinesTransformer(),
                new TrimWhitespaceTransformer())
        {
        }
    }
}