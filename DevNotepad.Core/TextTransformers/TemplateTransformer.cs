namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Template", "723c7c53-ff8a-4f3b-8c1f-0f8dd835c016")]
    public class TemplateTransformer : LineTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        private const string PlaceHolder = "%line%";

        public TemplateTransformer()
            : this(PlaceHolder)
        {
        }

        public TemplateTransformer(string template)
        {
            Template = template;
        }

        public string Template { get; set; }

        protected override string TransformLine(string line)
        {
            return string.IsNullOrWhiteSpace(Template)
                ? line
                : Template.Replace(PlaceHolder, line);
        }

        public object SaveState()
        {
            return Template;
        }

        public void RestoreState(object state)
        {
            Template = (string)state;
        }

        public string Parameter
        {
            get => Template;
            set => Template = value;
        }
    }
}