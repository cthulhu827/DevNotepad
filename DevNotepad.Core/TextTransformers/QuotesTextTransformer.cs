using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    public class QuotesTextTransformer : PipeTransformer
    {
        [TextTransformer("Single quoutes", "91e08820-c9c7-4b5f-971d-cf558f622cab", ShortCut = "Alt+S")]
        private static ITextTransformer BuildSingle() => new QuotesTextTransformer('\'');

        [TextTransformer("Double quoutes", "372d811d-f880-40df-8b34-f561a5b434fa", ShortCut = "Alt+D")]
        private static ITextTransformer BuildDouble() => new QuotesTextTransformer('"');

        public QuotesTextTransformer(char quote)
            : base(Init(quote))
        {
        }

        private static IEnumerable<ITextTransformer> Init(char quote)
        {
            yield return new NormalizeTextTransformer();
            yield return new TemplateTransformer($"{quote}%line%{quote}");
            yield return new EndWithSuffixTransformer(",", true);
        }

        protected override IEnumerable<ITextTransformer> Init()
        {
            yield break;
        }
    }
}