using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Double quoutes", "372d811d-f880-40df-8b34-f561a5b434fa")]
    public class DoubleQuotesTextTransformer : PipeTransformer
    {
        public DoubleQuotesTextTransformer() : base(Init())
        {
        }

        private static IEnumerable<ITextTransformer> Init()
        {
            yield return new NormalizeTextTransformer();

            // Строки уже могут быть с запятыми, поэтому проставляем запятые
            // во всех строках, чтобы на следующем шаге их отрезать.
            yield return new EndWithSuffixTransformer(",", false);

            yield return new TrimTextTransformer(null, null);
            yield return new TemplateTransformer("\"%line%\"");

            // Опять добавляем запятые кроме последней строки.
            yield return new EndWithSuffixTransformer(",", true);
        }
    }
}