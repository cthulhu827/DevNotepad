using System;
using System.Text;
using System.Text.RegularExpressions;

namespace DevNotepad.Core.TextTransformers
{
    public enum TextTransformType
    {
        ToLower,
        ToUpper,
        UnicodeEscape,
        UnicodeUnescape,
        PercentEncode,
        PercentDecode,
        Base64Encode,
        Base64Decode,
        JwtDecode,
        PythonEscape,
        PythonUnescape,
    }

    public class TextCaseTransformer : LineTransformer
    {
        [TextTransformer("To lower case", "bc1aab36-51ea-4798-896e-2e7e87132567")]
        private static ITextTransformer BuildLower() => new TextCaseTransformer(TextTransformType.ToLower);

        [TextTransformer("To upper case", "41f989ae-841d-4ea5-9c78-14db656d8abf")]
        private static ITextTransformer BuildUpper() => new TextCaseTransformer(TextTransformType.ToUpper);

        [TextTransformer("Unicode escape", "a3f7c1d2-5e84-4b96-8a01-2c3d4e5f6071")]
        private static ITextTransformer BuildUnicodeEscape() => new TextCaseTransformer(TextTransformType.UnicodeEscape);

        [TextTransformer("Unicode unescape", "b4e8d2c3-6f95-4ca7-9b12-3d4e5f607182")]
        private static ITextTransformer BuildUnicodeUnescape() => new TextCaseTransformer(TextTransformType.UnicodeUnescape);

        [TextTransformer("Percent encode", "c5f9e3d4-7a06-4db8-ac23-4e5f6071829a")]
        private static ITextTransformer BuildPercentEncode() => new TextCaseTransformer(TextTransformType.PercentEncode);

        [TextTransformer("Percent decode", "d6071e4e-8b17-5ec9-bd34-5f607182930b")]
        private static ITextTransformer BuildPercentUnencode() => new TextCaseTransformer(TextTransformType.PercentDecode);

        [TextTransformer("Base64 encode", "e7182f5f-9c28-6fd0-ce45-607182930c1c")]
        private static ITextTransformer BuildBase64Encode() => new TextCaseTransformer(TextTransformType.Base64Encode);

        [TextTransformer("Base64 decode", "f8293060-ad39-70e1-df56-7182930d2d2d")]
        private static ITextTransformer BuildBase64Decode() => new TextCaseTransformer(TextTransformType.Base64Decode);

        [TextTransformer("JWT decode", "a9304171-be4a-81f2-e067-8293041e3e3e")]
        private static ITextTransformer BuildJwtDecode() => new TextCaseTransformer(TextTransformType.JwtDecode);

        [TextTransformer("Python escape", "b0415282-cf5b-4293-a178-930415f4c4b3")]
        private static ITextTransformer BuildPythonEscape() => new TextCaseTransformer(TextTransformType.PythonEscape);

        [TextTransformer("Python unescape", "c1526393-d06c-4384-b289-041526a5d4c2")]
        private static ITextTransformer BuildPythonUnescape() => new TextCaseTransformer(TextTransformType.PythonUnescape);

        private const string IncorrectString = "Incorrect string";

        private readonly TextTransformType transformType;

        private TextCaseTransformer(TextTransformType transformType)
        {
            this.transformType = transformType;
        }

        protected override string TransformLine(string line)
        {
            return transformType switch
            {
                TextTransformType.ToLower => line.ToLower(),
                TextTransformType.ToUpper => line.ToUpper(),
                TextTransformType.UnicodeEscape => UnicodeEscape(line),
                TextTransformType.UnicodeUnescape => UnicodeUnescape(line),
                TextTransformType.PercentEncode => Uri.EscapeDataString(line),
                TextTransformType.PercentDecode => PercentDecode(line),
                TextTransformType.Base64Encode => Convert.ToBase64String(Encoding.UTF8.GetBytes(line)),
                TextTransformType.Base64Decode => Base64Decode(line),
                TextTransformType.JwtDecode => JwtDecode(line),
                TextTransformType.PythonEscape => PythonEscape(line),
                TextTransformType.PythonUnescape => PythonUnescape(line),
                _ => line
            };
        }

        private static string PercentDecode(string line)
        {
            try { return Uri.UnescapeDataString(line); }
            catch { return IncorrectString; }
        }

        private static string Base64Decode(string line)
        {
            try { return Encoding.UTF8.GetString(Convert.FromBase64String(line)); }
            catch { return IncorrectString; }
        }

        private static string UnicodeEscape(string line)
        {
            var sb = new StringBuilder(line.Length);
            foreach (char c in line)
            {
                if (c > 127)
                    sb.Append($"\\u{(int)c:x4}");
                else
                    sb.Append(c);
            }

            return sb.ToString();
        }

        private static readonly Regex UnescapePattern = new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.Compiled);

        private static string UnicodeUnescape(string line)
        {
            return UnescapePattern.Replace(line, m => ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString());
        }

        private static string JwtDecode(string line)
        {
            if (line.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                line = line.Substring(7);

            var parts = line.Split('.');
            if (parts.Length < 2)
                return IncorrectString;

            var decoded = new string[2];
            for (int i = 0; i < 2; i++)
            {
                decoded[i] = Base64UrlDecodeToString(parts[i]);
                if (decoded[i] == null)
                    return IncorrectString;
            }

            return string.Join(" ", decoded);
        }

        private static string Base64UrlDecodeToString(string base64Url)
        {
            string s = base64Url.Replace('-', '+').Replace('_', '/');
            s = s.PadRight(s.Length + (4 - s.Length % 4) % 4, '=');
            try
            {
                return Encoding.UTF8.GetString(Convert.FromBase64String(s));
            }
            catch
            {
                return null;
            }
        }
        private static readonly Regex PythonUnescapePattern = new Regex(@"\\x([0-9a-fA-F]{2})", RegexOptions.Compiled);

        private static string PythonEscape(string line)
        {
            var bytes = Encoding.UTF8.GetBytes(line);
            var sb = new StringBuilder(bytes.Length);
            foreach (byte b in bytes)
            {
                if (b > 127)
                    sb.Append($"\\x{b:x2}");
                else
                    sb.Append((char)b);
            }
            return sb.ToString();
        }

        private static string PythonUnescape(string line)
        {
            try
            {
                var bytes = new System.Collections.Generic.List<byte>();
                int i = 0;
                while (i < line.Length)
                {
                    if (i + 3 < line.Length && line[i] == '\\' && line[i + 1] == 'x')
                    {
                        bytes.Add(Convert.ToByte(line.Substring(i + 2, 2), 16));
                        i += 4;
                    }
                    else
                    {
                        foreach (byte b in Encoding.UTF8.GetBytes(line[i].ToString()))
                            bytes.Add(b);
                        i++;
                    }
                }
                return Encoding.UTF8.GetString(bytes.ToArray());
            }
            catch { return IncorrectString; }
        }
    }
}