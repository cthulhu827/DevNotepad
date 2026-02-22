using System;
using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Trim suffix / prefix", "6ed321bd-80b0-49b9-9180-4982901f7cef")]
    public class TrimTextTransformer : BaseTextTransformer, IParametrizedTextTransformer
    {
        public TrimTextTransformer()
            : this(null, null)
        {
        }

        public TrimTextTransformer(string? prefix, string? suffix)
        {
            Prefix = prefix;
            Suffix = suffix;
        }

        /// <summary>
        /// Префикс, который нужно отрезать.
        /// </summary>
        /// <remarks>
        /// null и "" ведут себя по-разному:
        /// - null - нужно определить префикс самостоятельно
        /// - "" - ничего отрезать не нужно
        /// </remarks>
        public string? Prefix { get; set; }

        /// <summary>
        /// Суффикс, который нужно отрезать.
        /// </summary>
        /// <remarks>
        /// null и "" ведут себя по-разному:
        /// - null - нужно определить суффикс самостоятельно
        /// - "" - ничего отрезать не нужно
        /// </remarks>
        public string? Suffix { get; set; }

        public string? CalculatedPrefix { get; private set; }

        public string? CalculatedSuffix { get; private set; }

        public override string[] Transform(string[] lines)
        {
            // Если все строки одинаковые или коллекция пустая, то ничего не обрезаем.
            if (lines.Distinct().Count() < 2) return lines;

            CalculatedPrefix = Prefix ?? GetPrefix(lines);
            CalculatedSuffix = Suffix ?? GetSuffix(lines);

            var prefixLength = CalculatedPrefix.Length;
            var suffixLength = CalculatedSuffix.Length;

            return prefixLength == 0 && suffixLength == 0
                ? lines
                : lines
                    .Select(line => TrimLine(line, CalculatedPrefix, CalculatedSuffix))
                    .ToArray();
        }

        private static string GetPrefix(string[] lines)
        {
            var result = "";
            var firstLine = lines.First();
            int prefixLength = 1;
            while (true)
            {
                var prefix = firstLine.Substring(0, prefixLength);
                if (lines.All(line => line.StartsWith(prefix)))
                {
                    result = prefix;
                    prefixLength++;
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private static string GetSuffix(string[] lines)
        {
            var result = "";
            var firstLine = lines.First();
            int suffixLength = 1;
            while (true)
            {
                var suffix = firstLine.Substring(firstLine.Length - suffixLength, suffixLength);
                if (lines.All(line => line.EndsWith(suffix)))
                {
                    result = suffix;
                    suffixLength++;
                }
                else
                {
                    break;
                }
            }

            return result;
        }

        private static string TrimLine(string line, string prefix, string suffix)
        {
            var prefixLength = prefix.Length;
            var suffixLength = suffix.Length;

            if (prefixLength == 0 && suffixLength == 0) return line;
            if (line.Length < prefixLength + suffixLength) return line;

            if (!line.StartsWith(prefix)) prefixLength = 0;
            if (!line.EndsWith(suffix)) suffixLength = 0;
            return line.Substring(prefixLength, line.Length - prefixLength - suffixLength);
        }

        public object SaveState()
        {
            return new Tuple<string?, string?>(Prefix, Suffix);
        }

        public void RestoreState(object state)
        {
            (Prefix, Suffix) = (Tuple<string?, string?>)state;
        }
    }
}