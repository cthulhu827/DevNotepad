using DevNotepad.Core.Utils;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Log to metric name", "8dbfb09d-c8c6-49c8-9b5f-0889a12406f6")]
    public class LogToMetricTransformer : LineTransformer
    {
        private readonly VariableNameParser parser = new VariableNameParser();

        protected override string TransformLine(string line)
        {
            var tokens = parser.Parse(line, ConventionType.PascalCase);
            return parser.Build(tokens, ConventionType.SnakeCase);
        }
    }
}