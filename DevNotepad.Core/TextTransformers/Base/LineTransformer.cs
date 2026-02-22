using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    public abstract class LineTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            return lines.Select(TransformLine).ToArray();
        }

        protected abstract string TransformLine(string line);
    }
}