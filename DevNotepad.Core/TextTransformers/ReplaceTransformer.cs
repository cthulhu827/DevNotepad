using Newtonsoft.Json;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Replace", "a7eaa061-6a22-4ea8-8c77-2170a8d514af", ShortCut = "Alt+R")]
    public class ReplaceTransformer : LineTransformer, IParametrizedTextTransformer
    {
        protected override string TransformLine(string line)
        {
            // IsNullOrWhiteSpace(OldValue) не подходит, т.к. иногда нужно искать по пробелу.
            return string.IsNullOrWhiteSpace(line) || string.IsNullOrEmpty(OldValue)
                ? line
                : line.Replace(OldValue, NewValue);
        }

        [JsonProperty]
        public string OldValue { get; set; } = "";

        [JsonProperty]
        public string NewValue { get; set; } = "";
    }
}