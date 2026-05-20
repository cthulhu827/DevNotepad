using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DevNotepad.Core.TextTransformers
{
    public enum JsonTransformType
    {
        Escape,
        Unescape,
        Pretty,
        Compact,
    }

    public class JsonTransformer : BaseTextTransformer
    {
        [TextTransformer("Json Escape", "d09ca4e6-c498-4835-9ca9-254da6cd9102")]
        private static ITextTransformer BuildEscape() => new JsonTransformer(JsonTransformType.Escape);

        [TextTransformer("Json Unescape", "5c8ea65c-6e7d-46fb-a4ef-d4b375487322")]
        private static ITextTransformer BuildUnescape() => new JsonTransformer(JsonTransformType.Unescape);

        [TextTransformer("Json Pretty Print", "7a976801-fb86-4dd3-87d0-7526c3eb9091")]
        private static ITextTransformer BuildPretty() => new JsonTransformer(JsonTransformType.Pretty);

        [TextTransformer("Json Compact", "2a345a41-4dfa-49b9-9470-1c3d8835162f")]
        private static ITextTransformer BuildCompact() => new JsonTransformer(JsonTransformType.Compact);

        private readonly JsonTransformType transformType;

        public JsonTransformer(JsonTransformType transformType)
        {
            this.transformType = transformType;
        }

        public override string[] Transform(string[] lines)
        {
            var input = string.Join(Environment.NewLine, lines);

            return transformType switch
            {
                JsonTransformType.Escape => Escape(input),
                JsonTransformType.Unescape => Unescape(input),
                JsonTransformType.Pretty => Pretty(input),
                JsonTransformType.Compact => Compact(input),
                _ => throw new UnsupportedEnumValueException<JsonTransformType>(transformType),
            };
        }

        private static string[] Escape(string input)
        {
            try
            {
                JToken.Parse(input);
                var escaped = JsonConvert.ToString(input);
                return new[] { escaped.Substring(1, escaped.Length - 2) };
            }
            catch
            {
                return new[] { "Incorrect json" };
            }
        }

        private static string[] Unescape(string input)
        {
            try
            {
                var jsonStringLiteral = input.TrimStart().StartsWith("\"")
                    ? input
                    : $"\"{input}\"";
                var unescaped = JsonConvert.DeserializeObject<string>(jsonStringLiteral);
                JToken.Parse(unescaped);
                return unescaped.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);
            }
            catch
            {
                return new[] { "Incorrect json" };
            }
        }

        private static string[] Pretty(string input)
        {
            try
            {
                var token = JToken.Parse(input);
                var pretty = token.ToString(Formatting.Indented);
                return pretty.Split(new[] { "\r\n", "\n" }, System.StringSplitOptions.None);
            }
            catch
            {
                return new[] { "Incorrect json" };
            }
        }

        private static string[] Compact(string input)
        {
            try
            {
                var token = JToken.Parse(input);
                return new[] { token.ToString(Formatting.None) };
            }
            catch
            {
                return new[] { "Incorrect json" };
            }
        }
    }
}