using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Substring", "0abcf9fb-0e08-4934-a83e-a54ee5bc402e")]
    public class SubstringTransformer : LineTransformer, IParametrizedTextTransformer
    {
        [TextTransformer("Skip Start", "e78c5701-6e1c-432c-85fd-3bc3bcc9508a")]
        private static ITextTransformer BuildSkipStart() => new SubstringTransformer(string.Empty, SubstringType.SkipStart);

        [TextTransformer("Skip End", "671d9f83-8bec-4acc-ab8b-a80292065bbc")]
        private static ITextTransformer BuildSkipEnd() => new SubstringTransformer(string.Empty, SubstringType.SkipEnd);

        [TextTransformer("Take Start", "8959ac78-0cdf-42ae-b106-bcad6a95b78d")]
        private static ITextTransformer BuildTakeStart() => new SubstringTransformer(string.Empty, SubstringType.TakeStart);

        [TextTransformer("Take End", "8b9b532c-6615-4b61-a4d7-4243f5323d19")]
        private static ITextTransformer BuildTakeEnd() => new SubstringTransformer(string.Empty, SubstringType.TakeEnd);

        [TextTransformer("Take Before", "d92e6110-b1ed-4179-b970-92bd1a6ebccd")]
        private static ITextTransformer BuildTakeBefore() => new SubstringTransformer(string.Empty, SubstringType.TakeBefore);

        [TextTransformer("Take After", "ed5639df-6c54-4c01-bc96-60ef84d9e94e")]
        private static ITextTransformer BuildTakeAfter() => new SubstringTransformer(string.Empty, SubstringType.TakeAfter);

        [TextTransformer("File names for review", "ab9af1c7-2355-4d58-8f2b-8fdf9b0114cd")]
        private static ITextTransformer BuildFileNamesForReview() =>
            new SubstringTransformer(@"\dev\", SubstringType.TakeAfter) { DontEditNew = true };

        public SubstringTransformer()
            : this(string.Empty, SubstringType.TakeStart)
        {
        }

        public SubstringTransformer(string limit, SubstringType type)
        {
            Limit = limit;
            Type = type;
        }

        public string Limit { get; set; }

        public SubstringType Type { get; set; }

        public bool DontEditNew { get; private set; }

        protected override string TransformLine(string line)
        {
            if (string.IsNullOrWhiteSpace(Limit) || string.IsNullOrWhiteSpace(line)) return line;

            return Type switch
            {
                SubstringType.SkipStart => SubstringByLimit(line),
                SubstringType.SkipEnd => SubstringByLimit(line),
                SubstringType.TakeStart => SubstringByLimit(line),
                SubstringType.TakeEnd => SubstringByLimit(line),
                SubstringType.TakeBefore => SubstringBySeparator(line),
                SubstringType.TakeAfter => SubstringBySeparator(line),
                _ => line
            };
        }

        public object SaveState()
        {
            return new Tuple<string, SubstringType>(Limit, Type);
        }

        public void RestoreState(object state)
        {
            (Limit, Type) = (Tuple<string, SubstringType>)state;
        }

        private string SubstringByLimit(string line)
        {
            var length = ParseLimit(Limit);
            if (length == 0) return line;

            return line.Length < length
                ? line
                : Type switch
                {
                    SubstringType.SkipStart => line[length..],
                    SubstringType.SkipEnd => line[..^length],
                    SubstringType.TakeStart => line[..length],
                    SubstringType.TakeEnd => line[^length..],
                    _ => line
                };
        }

        private string SubstringBySeparator(string line)
        {
            return Type switch
            {
                SubstringType.TakeBefore => TakeBefore(line),
                SubstringType.TakeAfter => TakeAfter(line),
                _ => line
            };
        }

        private static int ParseLimit(string limit)
        {
            if (string.IsNullOrWhiteSpace(limit))
            {
                return 0;
            }

            if (int.TryParse(limit, out var result))
            {
                return result;
            }

            return limit.Length;
        }

        private string TakeBefore(string line)
        {
            var pos = line.IndexOf(Limit, StringComparison.CurrentCulture);
            return pos == -1 ? line : line.Substring(0, pos);
        }

        private string TakeAfter(string line)
        {
            var pos = line.IndexOf(Limit, StringComparison.CurrentCulture);
            return pos == -1 ? line : line.Substring(pos + Limit.Length);
        }
    }
}