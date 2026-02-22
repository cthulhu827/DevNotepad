using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Single quoutes", "91e08820-c9c7-4b5f-971d-cf558f622cab")]
    public class SingleQuotesTextTransformer : PipeTransformer
    {
        public SingleQuotesTextTransformer() : base(Init())
        {
        }

        private static IEnumerable<ITextTransformer> Init()
        {
            yield return new NormalizeTextTransformer();

            // Строки уже могут быть с запятыми, поэтому проставляем запятые
            // во всех строках, чтобы на следующем шаге их отрезать.
            yield return new EndWithSuffixTransformer(",", false);

            yield return new TrimTextTransformer(null, null);
            yield return new TemplateTransformer("'%line%'");

            // Опять добавляем запятые кроме последней строки.
            yield return new EndWithSuffixTransformer(",", true);
        }
    }
}