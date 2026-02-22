namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Join", "ce7ec348-f3b3-4ae4-9c32-73d9e6bb7ba2")]
    public class JoinTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        private string separator = string.Empty;

        public override string[] Transform(string[] lines)
        {
            return new[] { string.Join(separator, lines) };
        }

        public object SaveState()
        {
            return separator;
        }

        public void RestoreState(object state)
        {
            separator = (string)state;
        }

        public string Parameter
        {
            get => separator;
            set => separator = value;
        }
    }
}