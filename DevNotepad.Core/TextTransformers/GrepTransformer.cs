using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Grep", "acbf51fd-f580-42b0-a6d3-08ba127a3062")]
    public class GrepTransformer : BaseTextTransformer, IParametrizedTextTransformer
    {
        [TextTransformer("Exclude", "19d89fa0-4155-4500-8d0d-47d4d5ef6fad")]
        private static ITextTransformer BuildExclude() => new GrepTransformer(string.Empty) { Exclude = true };

        [TextTransformer("Regular Expression (RegEx)", "9f180703-3b94-4aff-b465-49fd83354ca4")]
        private static ITextTransformer BuildRegEx() => new GrepTransformer(string.Empty) { RegEx = true };

        public GrepTransformer()
            : this(string.Empty)
        {
        }

        public GrepTransformer(string searchText)
        {
            SearchText = searchText;
        }

        public string SearchText { get; set; }
        public bool Exclude { get; set; }
        public bool CaseSensitive { get; set; }
        public bool RegEx { get; set; }

        public override string[] Transform(string[] lines)
        {
            if (string.IsNullOrWhiteSpace(SearchText)) return lines;

            if (RegEx)
            {
                Regex regEx;
                try
                {
                    regEx = new Regex(SearchText);
                }
                catch (Exception)
                {
                    return new[] { "Invalid RegEx: " + SearchText };
                }

                return lines
                    .Select(line => CheckRegEx(line, regEx))
                    .Where(line => line != null)
                    .Select(line => line!)
                    .ToArray();
            }

            var comparsion = CaseSensitive
                ? StringComparison.InvariantCulture
                : StringComparison.InvariantCultureIgnoreCase;
            return lines
                .Where(line => line.Contains(SearchText, comparsion) != Exclude)
                .ToArray();
        }

        public object SaveState()
        {
            return new Tuple<string, bool, bool, bool>(SearchText, Exclude, CaseSensitive, RegEx);
        }

        public void RestoreState(object state)
        {
            (SearchText, Exclude, CaseSensitive, RegEx) = (Tuple<string, bool, bool, bool>)state;
        }

        private string? CheckRegEx(string line, Regex regEx)
        {
            var match = regEx.Match(line);

            // Если включена опция Exclude, то просто находим строки по RegEx'у и исключаем их,
            // а неподходящие по RegEx'у строки оставляем как есть.
            if (Exclude)
            {
                return match.Success ? null : line;
            }

            // Иначе ищем строки по RegEx'у и возвращаем их как есть, если в RegEx'е не указывается
            // группа. При указании группы возвращаем не всю строку целиком, а только найденную группу.
            if (match.Success)
            {
                return match.Groups.Count == 1 ? line : match.Groups[1].Value;
            }

            return null;
        }
    }
}