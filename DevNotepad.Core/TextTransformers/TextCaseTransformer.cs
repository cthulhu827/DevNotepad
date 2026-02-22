namespace DevNotepad.Core.TextTransformers
{
    public class TextCaseTransformer : LineTransformer
    {
        [TextTransformer("To lower case", "bc1aab36-51ea-4798-896e-2e7e87132567")]
        private static ITextTransformer BuildLower() => new TextCaseTransformer(true);

        [TextTransformer("To upper case", "41f989ae-841d-4ea5-9c78-14db656d8abf")]
        private static ITextTransformer BuildUpper() => new TextCaseTransformer(false);

        private readonly bool toLower;

        private TextCaseTransformer(bool toLower)
        {
            this.toLower = toLower;
        }

        protected override string TransformLine(string line)
        {
            return toLower ? line.ToLower() : line.ToUpper();
        }
    }
}