using System;
using System.Collections.Generic;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Nth Lines", "f691835b-7948-48d1-974a-5cc11e560ec4")]
    public class NthLinesTransformer : BaseTextTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        [TextTransformer("Odd Lines", "33039a27-b89b-45f3-a4e0-16f66e951c20")]
        private static ITextTransformer BuildOdd() => new NthLinesTransformer { Parameter = "2/1", DontEditNew = true };

        [TextTransformer("Even Lines", "9267e72f-9f3b-4407-9112-5291165e2425")]
        private static ITextTransformer BuildEven() => new NthLinesTransformer { Parameter = "2/2", DontEditNew = true };

        private string expression = string.Empty;

        private int groupSize;
        private int[] lineNumbers = Array.Empty<int>();

        public override string[] Transform(string[] lines)
        {
            if (groupSize == 0) return lines;

            var result = new List<string>();

            for (var groupStart = 0; groupStart < lines.Length; groupStart += groupSize)
            {
                var currentGroupSize = Math.Min(groupSize, lines.Length - groupStart);

                foreach (var lineNumber in lineNumbers)
                {
                    var indexInGroup = lineNumber - 1;
                    if (indexInGroup < currentGroupSize)
                        result.Add(lines[groupStart + indexInGroup]);
                }
            }

            return result.ToArray();
        }

        public object SaveState()
        {
            return expression;
        }

        public void RestoreState(object state)
        {
            Parameter = (string)state;
        }

        public bool DontEditNew { get; private set; }

        public string Parameter
        {
            get => expression;
            set
            {
                expression = value;
                (groupSize, lineNumbers) = ParseExpression(expression);
            }
        }

        public string Hint => "<Group size>/<Line numbers> 🡒 6/1-3,6";

        private static (int, int[]) ParseExpression(string expression)
        {
            var invalid = (0, Array.Empty<int>());

            if (string.IsNullOrWhiteSpace(expression))
            {
                return invalid;
            }

            var parts = expression.Split('/');
            if (parts.Length != 2)
            {
                return invalid;
            }

            var lineNumbersPart = parts[1].Trim();
            var groupSizePart = parts[0].Trim();

            if (string.IsNullOrWhiteSpace(lineNumbersPart) || string.IsNullOrWhiteSpace(groupSizePart))
            {
                return invalid;
            }

            if (!int.TryParse(groupSizePart, out var groupSize) || groupSize <= 0)
            {
                return invalid;
            }

            var result = new List<int>();
            var tokens = lineNumbersPart.Split(',');

            foreach (var rawToken in tokens)
            {
                var token = rawToken.Trim();
                if (string.IsNullOrWhiteSpace(token))
                {
                    return invalid;
                }

                if (token.Contains("-"))
                {
                    var bounds = token.Split('-');
                    if (bounds.Length != 2)
                    {
                        return invalid;
                    }

                    if (!int.TryParse(bounds[0].Trim(), out var from) || !int.TryParse(bounds[1].Trim(), out var to))
                    {
                        return invalid;
                    }

                    if (from <= 0 || to <= 0 || from > to)
                    {
                        return invalid;
                    }

                    if (from > groupSize || to > groupSize)
                    {
                        return invalid;
                    }

                    for (var i = from; i <= to; i++)
                    {
                        result.Add(i);
                    }
                }
                else
                {
                    if (!int.TryParse(token, out var value))
                    {
                        return invalid;
                    }

                    if (value <= 0 || value > groupSize)
                    {
                        return invalid;
                    }

                    result.Add(value);
                }
            }

            if (result.Count == 0)
            {
                return invalid;
            }

            return (groupSize, result.ToArray());
        }
    }
}