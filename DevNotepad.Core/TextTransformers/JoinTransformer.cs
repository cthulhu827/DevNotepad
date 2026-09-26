using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Join", "ce7ec348-f3b3-4ae4-9c32-73d9e6bb7ba2")]
    public class JoinTransformer : BaseTextTransformer, IParametrizedTextTransformer
    {
        [TextTransformer("Join with comma", "6b759eaf-b956-4991-99b9-647648475cc1")]
        private static ITextTransformer BuildJoinWithComma() => new JoinTransformer { Separator = ", ", DontEditNew = true };

        [TextTransformer("Join pairs", "dca20309-d398-4279-a842-2af8daa72fa4")]
        private static ITextTransformer BuildJoinPairs() => new JoinTransformer { Separator = " / ", GroupSize = 2, DontEditNew = false };

        [JsonProperty]
        public string Separator { get; set; } = string.Empty;

        [JsonProperty]
        public int GroupSize { get; set; } = 1;

        [JsonProperty]
        public bool Template { get; set; }

        public string GetLinePlaceHolder(int idx)
        {
            return GroupSize < 2 ? string.Empty : $"%{idx + 1}%";
        }

        public override string[] Transform(string[] lines)
        {
            if (GroupSize < 1) return lines;

            if (GroupSize == 1)
            {
                return new[] { string.Join(Separator, lines) };
            }

            var result = new List<string>();
            for (int i = 0; i < lines.Length; i += GroupSize)
            {
                var count = Math.Min(GroupSize, lines.Length - i);
                var grp = lines.Skip(i).Take(count).ToArray();
                if (Template)
                {
                    var resultLine = Separator;
                    for (int j = 0; j < grp.Length; j++)
                    {
                        resultLine = resultLine.Replace(GetLinePlaceHolder(j), grp[j]);
                    }

                    result.Add(resultLine);
                }
                else
                    result.Add(grp.Join(Separator));
            }

            return result.ToArray();
        }

        public bool DontEditNew { get; private set; }
    }
}