using Newtonsoft.Json;
using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Split", "9788f313-fa5e-4cd7-aec0-8edc665e14f3")]
    public class SplitTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        [JsonProperty]
        public string Separator { get; set; } = string.Empty;

        public override string[] Transform(string[] lines)
        {
            return string.IsNullOrEmpty(Separator) // именно Empty, по whitespace можно разделять
                ? lines
                : lines.SelectMany(line => line.Split(Separator)).ToArray();
        }

        public string Parameter
        {
            get => Separator;
            set => Separator = value;
        }
    }
}