using Newtonsoft.Json;
using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Padding", "284bfb82-4e2c-4c24-a4be-ddd6da6a40a3")]
    public class PaddingTransformer : LineTransformer, IParametrizedTextTransformer
    {
        [JsonProperty]
        public string TotalLength { get; set; } = string.Empty;

        [JsonProperty]
        public char Symbol { get; set; } = ' ';

        [JsonProperty]
        public PaddingType PaddingType { get; set; }

        protected override string TransformLine(string line)
        {
            if (Symbol == '\0') return line;

            var totalLength = ParseLength(TotalLength);
            if (totalLength == 0 || line.Length >= totalLength) return line;

            return PaddingType == PaddingType.Leading
                ? line.PadLeft(totalLength, Symbol)
                : line.PadRight(totalLength, Symbol);
        }

        private static int ParseLength(string length) // todo: дублируется в SubstringTransformer
        {
            if (string.IsNullOrWhiteSpace(length))
            {
                return 0;
            }

            if (int.TryParse(length, out var result))
            {
                return result;
            }

            return length.Length;
        }
    }
}