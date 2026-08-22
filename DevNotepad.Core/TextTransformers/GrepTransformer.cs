using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Grep", "acbf51fd-f580-42b0-a6d3-08ba127a3062", ShortCut = "Alt+G")]
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

        [JsonProperty]
        public string SearchText { get; set; }

        [JsonProperty]
        public bool Exclude { get; set; }

        [JsonProperty]
        public bool CaseSensitive { get; set; }

        [JsonProperty]
        public bool RegEx { get; set; }

        [JsonProperty]
        public int LinesBefore { get; set; }

        [JsonProperty]
        public int LinesAfter { get; set; }

        [JsonProperty]
        public bool DoNotSeparate { get; set; }

        public override string[] Transform(string[] lines)
        {
            if (string.IsNullOrWhiteSpace(SearchText) || !lines.Any()) return lines;

            // Ищем подходящие строки, проставляем им признак NeedReturn.
            LineResult[] needReturn;
            IDictionary<int, string>? regExGroups = RegEx ? new Dictionary<int, string>() : null;
            try
            {
                needReturn = SearchBySearchText(lines, regExGroups);
            }
            catch (Exception)
            {
                return new[] { "Invalid RegEx: " + SearchText };
            }

            MarkLinesAround(needReturn);

            InverseIfExclude(needReturn);

            AddEmptyLinesBetweenFoundGroups(needReturn);

            // Возвращаем результат.
            return BuildResult(lines, needReturn, regExGroups);
        }

        private void MarkLinesAround(LineResult[] needReturn)
        {
            if (LinesAfter == 0 && LinesBefore == 0) return;

            // Проставляем строкам до и после найденных признак IncludeAround.
            // Сразу проставлять Include нельзя, т.к. если его проставить строке,
            // которая идёт после найденной, то на следующей итерации цикла эта строка
            // тоже будет считаться найденной и для неё также сработает настройка LinesAfter.
            for (int i = 0; i < needReturn.Length; i++)
            {
                if (needReturn[i] == LineResult.Exclude || needReturn[i] == LineResult.IncludeAround) continue;

                var lo = Math.Max(i - LinesBefore, 0);
                var hi = Math.Min(i + LinesAfter + 1, needReturn.Length);
                for (int j = lo; j < hi; j++)
                {
                    if (needReturn[j] == LineResult.Exclude) needReturn[j] = LineResult.IncludeAround;
                }
            }

            // Всем строкам с признаком IncludeAround меняем его на Include.
            for (int i = 0; i < needReturn.Length; i++)
                if (needReturn[i] == LineResult.IncludeAround)
                    needReturn[i] = LineResult.Include;
        }

        private void AddEmptyLinesBetweenFoundGroups(LineResult[] needReturn)
        {
            if (LinesAfter == 0 && LinesBefore == 0 || DoNotSeparate) return;

            // После каждой из сформированных групп строк добавляем пустую строку.
            // Если группы пересекаются или идут подряд, то пустая строка не добавляется.
            for (int i = 1; i < needReturn.Length; i++)
            {
                var prev = needReturn[i - 1];
                if (needReturn[i] == LineResult.Exclude &&
                    (prev == LineResult.Include || prev == LineResult.IncludeRegEx))
                    needReturn[i] = LineResult.Empty;
            }

            // Убираем незначащие строки в конце. Exclude оставляем как есть,
            // а Empty заменяем на Exclude.
            for (int i = needReturn.Length - 1; i >= 0; i--)
            {
                if (needReturn[i] == LineResult.Exclude)
                    continue;

                if (needReturn[i] == LineResult.Empty)
                    needReturn[i] = LineResult.Exclude;
                else
                    break;
            }
        }

        private string[] BuildResult(string[] lines, LineResult[] needReturn, IDictionary<int, string>? regExGroups)
        {
            var result = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                switch (needReturn[i])
                {
                    // IncludeAround не ожидаем, т.к. он должен быть заменён на Include
                    case LineResult.Exclude:
                        break;
                    case LineResult.Include:
                        result.Add(lines[i]);
                        break;
                    case LineResult.IncludeRegEx:
                        result.Add(regExGroups![i]);
                        break;
                    case LineResult.Empty:
                        result.Add(string.Empty);
                        break;
                    default:
                        throw new UnsupportedEnumValueException<LineResult>(needReturn[i]);
                }
            }

            return result.ToArray();
        }

        private void InverseIfExclude(LineResult[] needReturn)
        {
            if (!Exclude) return;

            for (int i = 0; i < needReturn.Length; i++)
            {
                if (needReturn[i] == LineResult.Exclude)
                    needReturn[i] = LineResult.Include;
                else
                    needReturn[i] = LineResult.Exclude;
            }
        }

        private LineResult[] SearchBySearchText(string[] lines, IDictionary<int, string>? regExGroups)
        {
            if (RegEx)
            {
                var result = new LineResult[lines.Length];
                var regEx = new Regex(SearchText);
                for (int i = 0; i < lines.Length; i++)
                {
                    var match = regEx.Match(lines[i]);
                    if (match.Success)
                    {
                        // Ищем строки по RegEx'у и возвращаем их как есть, если в RegEx'е не указывается группа.
                        // При указании группы возвращаем не всю строку целиком, а только найденную группу.
                        if (match.Groups.Count == 1)
                            result[i] = LineResult.Include;
                        else
                        {
                            result[i] = LineResult.IncludeRegEx;
                            regExGroups![i] = match.Groups[1].Value;
                        }
                    }
                    else
                        result[i] = LineResult.Exclude;
                }

                return result;
            }

            var comparsion = CaseSensitive
                ? StringComparison.InvariantCulture
                : StringComparison.InvariantCultureIgnoreCase;
            return lines
                .Select(line => line.Contains(SearchText, comparsion) ? LineResult.Include : LineResult.Exclude)
                .ToArray();
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

        private enum LineResult
        {
            Exclude,
            Include,
            IncludeRegEx,
            IncludeAround,
            Empty,
        }
    }
}