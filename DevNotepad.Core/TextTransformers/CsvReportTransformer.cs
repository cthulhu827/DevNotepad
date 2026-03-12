using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Elastic CSV report", "0e29bce9-e07d-44db-ac08-83349fec0c3e")]
    public class CsvReportTransformer : PipeTransformer
    {
        protected override IEnumerable<ITextTransformer> Init()
        {
            // todo: skip first line transformer
            yield return new GrepTransformer("@timestamp") { Exclude = true };
            yield return new SubstringTransformer(1, SubstringType.SkipEnd);
            yield return new SubstringTransformer(36, SubstringType.TakeEnd);
        }
    }
}