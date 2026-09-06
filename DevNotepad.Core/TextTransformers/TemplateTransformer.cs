using System;
using System.Linq;
using Newtonsoft.Json;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Template", "723c7c53-ff8a-4f3b-8c1f-0f8dd835c016", ShortCut = "Alt+T")]
    public class TemplateTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        private const string LinePlaceHolder = "%line%";
        private const string LineIndexPlaceHolder = "%idx%";

        public TemplateTransformer()
            : this(LinePlaceHolder)
        {
        }

        public TemplateTransformer(string template)
        {
            Template = template;
        }

        [JsonProperty]
        public string Template { get; set; }

        public override string[] Transform(string[] lines)
        {
            if (string.IsNullOrWhiteSpace(Template) || lines.IsEmpty()) return lines;

            return lines.Select(TransformLine).ToArray();
        }

        private string TransformLine(string line, int index)
        {
            return string.IsNullOrWhiteSpace(Template)
                ? line
                : Template
                    .Replace(LinePlaceHolder, line)
                    .Replace(LineIndexPlaceHolder, (index + 1).ToString());
        }

        public string Parameter
        {
            get => Template;
            set => Template = value;
        }
    }
}