using DevNotepad.Core.Utils;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Metric to log name", "0632b85c-6d59-4029-bdf1-c087ce7a8f5a")]
    public class MetricToLogTransformer : LineTransformer
    {
        private readonly VariableNameParser parser = new VariableNameParser();

        protected override string TransformLine(string line)
        {
            var tokens = parser.Parse(line, ConventionType.SnakeCase);
            return parser.Build(tokens, ConventionType.PascalCase);
        }
    }
}