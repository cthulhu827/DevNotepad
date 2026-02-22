using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Split", "9788f313-fa5e-4cd7-aec0-8edc665e14f3")]
    public class SplitTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        private string separator = string.Empty;

        public override string[] Transform(string[] lines)
        {
            return string.IsNullOrEmpty(separator) // именно Empty, по whitespace можно разделять
                ? lines
                : lines.SelectMany(line => line.Split(separator)).ToArray();
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