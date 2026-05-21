using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Replace", "a7eaa061-6a22-4ea8-8c77-2170a8d514af")]
    public class ReplaceTransformer : LineTransformer, IParametrizedTextTransformer
    {
        protected override string TransformLine(string line)
        {
            return string.IsNullOrWhiteSpace(line) || string.IsNullOrWhiteSpace(OldValue)
                ? line
                : line.Replace(OldValue, NewValue);
        }

        public string OldValue { get; set; } = "";

        public string NewValue { get; set; } = "";

        public object SaveState()
        {
            return new Tuple<string, string>(OldValue, NewValue);
        }

        public void RestoreState(object state)
        {
            (OldValue, NewValue) = (Tuple<string, string>)state;
        }
    }
}