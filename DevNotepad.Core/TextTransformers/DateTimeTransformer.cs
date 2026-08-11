using System;
using System.Collections.Generic;
using DtConverter;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Date-time converter", "37b9029f-076a-4570-996f-a26a022dbff3", ShortCut = "Alt+Ctrl+D")]
    public class DateTimeTransformer : BaseTextTransformer, ISingleParameterTextTransformer, IParametrizedTextTransformer
    {
        public string LineSuffix { get; set; } = "";

        public override string[] Transform(string[] lines)
        {
            var result = new List<string>();

            IWrapper? prev = null;
            var dtParser = new LexicalParser(new NowProvider());
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    result.Add(string.Empty);
                    continue;
                }

                // Если строка начинается с "!", то игнорируем LineSuffix
                var lineToParse = line.StartsWith("!") ? line[1..] : line + LineSuffix;
                try
                {
                    var wrapper = dtParser.Parse(prev, lineToParse);
                    if (wrapper == null)
                        result.Add("error");
                    else
                    {
                        result.Add(wrapper.ToString());
                        prev = wrapper;
                    }
                }
                catch (Exception e)
                {
                    result.Add(e.Message);
                }
            }

            return result.ToArray();
        }

        public string Parameter
        {
            get => LineSuffix;
            set => LineSuffix = value;
        }

        public object SaveState()
        {
            return LineSuffix;
        }

        public void RestoreState(object state)
        {
            LineSuffix = (string)state;
        }

        public bool DontEditNew => true;
    }
}