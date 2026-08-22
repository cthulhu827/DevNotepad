using Newtonsoft.Json;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Join", "ce7ec348-f3b3-4ae4-9c32-73d9e6bb7ba2")]
    public class JoinTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        [JsonProperty]
        public string Separator { get; set; } = string.Empty;

        public override string[] Transform(string[] lines)
        {
            return new[] { string.Join(Separator, lines) };
        }

        public string Parameter
        {
            get => Separator;
            set => Separator = value;
        }
    }
}