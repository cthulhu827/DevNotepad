using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("AppSettings Linearizer", "3214fcc7-eedd-4273-badc-5163980a19de")]
    public class AppSettingsTransformer : BaseTextTransformer
    {
        public override string[] Transform(string[] lines)
        {
            if (lines.IsEmpty()) return lines;

            var json = string.Join(Environment.NewLine, lines);
            var result = new List<string>();

            var token = JToken.Parse(json);
            FlattenJson(token, "", result);

            return result.ToArray();
        }

        private void FlattenJson(JToken token, string prefix, List<string> result)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    foreach (JProperty property in token.Children<JProperty>())
                    {
                        var newPrefix = string.IsNullOrEmpty(prefix)
                            ? property.Name
                            : $"{prefix}__{property.Name}";
                        FlattenJson(property.Value, newPrefix, result);
                    }
                    break;

                case JTokenType.Array:
                    int index = 0;
                    foreach (JToken item in token.Children())
                    {
                        FlattenJson(item, $"{prefix}__{index}", result);
                        index++;
                    }
                    break;

                default:
                    // Примитивное значение (string, number, boolean, null)
                    result.Add($"{prefix} = {token}");
                    break;
            }
        }
    }
}