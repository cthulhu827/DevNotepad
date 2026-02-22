using System;
using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    /// <summary>
    /// Добавляет указанный суффикс в конец каждой строки, если там его ещё нет.
    /// В последнюю строку добавляется опционально.
    /// </summary>
    [TextTransformer("End with suffix", "e1c6568e-2443-4fe0-b2ad-0abf043d8ff8")]
    public class EndWithSuffixTransformer : BaseTextTransformer, IParametrizedTextTransformer
    {
        public EndWithSuffixTransformer()
            : this(string.Empty, false)
        {
        }

        public EndWithSuffixTransformer(string suffix, bool exceptLastLine)
        {
            Suffix = suffix;
            ExceptLastLine = exceptLastLine;
        }

        public string Suffix { get; set; }

        public bool ExceptLastLine { get; set; }

        public override string[] Transform(string[] lines)
        {
            if (string.IsNullOrWhiteSpace(Suffix)) return lines;

            if (lines.Length < 2) return lines;

            return lines
                .Select((line, idx) => TransformLine(line, idx == lines.Length - 1))
                .ToArray();
        }

        private string TransformLine(string line, bool isLast)
        {
            if (isLast && ExceptLastLine)
                return line.EndsWith(Suffix) ? line.Substring(0, line.Length - Suffix.Length) : line;

            return line.EndsWith(Suffix) ? line : line + Suffix;
        }

        public object SaveState()
        {
            return new Tuple<string, bool>(Suffix, ExceptLastLine);
        }

        public void RestoreState(object state)
        {
            (Suffix, ExceptLastLine) = (Tuple<string, bool>)state;
        }
    }
}