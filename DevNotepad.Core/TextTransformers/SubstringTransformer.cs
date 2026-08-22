using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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

        [TextTransformer("Take Before", "d92e6110-b1ed-4179-b970-92bd1a6ebccd", ShortCut = "Alt+B")]
        private static ITextTransformer BuildTakeBefore() => new SubstringTransformer(string.Empty, SubstringType.TakeBefore);

        [TextTransformer("Take After", "ed5639df-6c54-4c01-bc96-60ef84d9e94e", ShortCut = "Alt+A")]
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

        public SubstringTransformer(int limit, SubstringType type)
            : this(limit.ToString(), type)
        {
        }

        [JsonProperty]
        public string Limit { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        public SubstringType Type { get; set; }

        [JsonProperty]
        [JsonConverter(typeof(ValueTupleConverter))]
        public (int SelStart, int SelLength) Selection { get; set; }

        public bool DontEditNew { get; private set; }

        protected override string TransformLine(string line)
        {
            // IsNullOrWhiteSpace(Limit) не подходит, т.к. иногда нужно искать по пробелу.
            if (string.IsNullOrEmpty(Limit) || string.IsNullOrWhiteSpace(line)) return line;

            return Type switch
            {
                SubstringType.SkipStart => SubstringByLimit(line),
                SubstringType.SkipEnd => SubstringByLimit(line),
                SubstringType.TakeStart => SubstringByLimit(line),
                SubstringType.TakeEnd => SubstringByLimit(line),
                SubstringType.TakeBefore => SubstringBySeparator(line),
                SubstringType.TakeAfter => SubstringBySeparator(line),
                SubstringType.BySelection => SubstringBySelection(line),
                _ => line
            };
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

        private string SubstringBySelection(string line)
        {
            return Selection.SelLength == 0 || Selection.SelStart + Selection.SelLength > line.Length
                ? line
                : line.Substring(Selection.SelStart, Selection.SelLength);
        }

        private static readonly Regex IntWithPlusesPattern = new Regex(@"^(\d+)(\+)+$", RegexOptions.Compiled);
        private static readonly Regex IntWithMinusesPattern = new Regex(@"^(\d+)(\-)+$", RegexOptions.Compiled);

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

            var match = IntWithPlusesPattern.Match(limit);
            if (match.Success)
            {
                var baseValue = int.Parse(match.Groups[1].Value);
                var plusCount = match.Groups[2].Captures.Count;
                return baseValue + plusCount;
            }

            match = IntWithMinusesPattern.Match(limit);
            if (match.Success)
            {
                var baseValue = int.Parse(match.Groups[1].Value);
                var minusCount = match.Groups[2].Captures.Count;
                result = baseValue - minusCount;
                if (result < 0) result = 0;
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

    /// <summary>
    /// JsonConverter for <see cref="ValueTuple{T1,T2}"/> — Newtonsoft.Json does not serialize
    /// the Item1/Item2 fields of ValueTuple by default, so a custom converter is needed.
    /// </summary>
    internal class ValueTupleConverter : JsonConverter<(int, int)>
    {
        public override void WriteJson(JsonWriter writer, (int, int) value, JsonSerializer serializer)
        {
            writer.WriteStartArray();
            writer.WriteValue(value.Item1);
            writer.WriteValue(value.Item2);
            writer.WriteEndArray();
        }

        public override (int, int) ReadJson(
            JsonReader reader, Type objectType, (int, int) existingValue, bool hasExistingValue,
            JsonSerializer serializer)
        {
            var arr = serializer.Deserialize<int[]>(reader);
            if (arr == null || arr.Length < 2)
                return default;
            return (arr[0], arr[1]);
        }
    }
}