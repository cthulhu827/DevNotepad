using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Sum", "5394b8ae-f20b-4285-99ac-11b45441f574")]
    public class SumTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            long result = 0;
            foreach (var line in lines)
            {
                if (long.TryParse(line, out var number))
                    result += number;
                else
                    return $"Can't parse '{line}'".AsArray();
            }

            return result.ToString().AsArray();
        }
    }
}