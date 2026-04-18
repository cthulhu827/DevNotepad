using System;
using System.Linq;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("File size", "3b5f7e8a-9c2d-4a1f-b6e3-7d8c9a1e2f4b")]
    public class FileSizeTransformer : LineTransformer, IParametrizedTextTransformer, ISingleParameterTextTransformer
    {
        private static readonly SizeDescription[] SupportedFormats =
        {
            new SizeDescription('t', 4, "Tb"),
            new SizeDescription('g', 3, "Gb"),
            new SizeDescription('m', 2, "Mb"),
            new SizeDescription('k', 1, "Kb")
        };

        public string Format { get; set; } = string.Empty;

        protected override string TransformLine(string line)
        {
            // Преобразование в зависимости от формата
            if (string.IsNullOrWhiteSpace(line) || string.IsNullOrWhiteSpace(Format)) return line;

            // Попытка распарсить строку в целое число
            if (!long.TryParse(line, out long sizeInBytes)) return $"Can't parse '{line}'";

            // Специальный формат "," - разбиение по тысячам
            if (Format == ",")
            {
                return FormatWithThousandsSeparator(sizeInBytes);
            }

            // Парсим формат
            var (sizeDesc, precision, appendUnit) = ParseFormat(Format.ToLower());

            return sizeDesc == null
                ? $"Incorrect format '{Format}'"
                : ConvertSize(sizeInBytes, sizeDesc.Power, sizeDesc.Unit, precision, appendUnit);
        }

        public object SaveState()
        {
            return Format;
        }

        public void RestoreState(object state)
        {
            Format = (string)state;
        }

        public string Parameter
        {
            get => Format;
            set => Format = value;
        }

        public string Hint => ", 🡒 1 000 000\ng2+ 🡒 1.23 Gb    g2 🡒 1.23    g+ 🡒 1 Gb    g 🡒 1";

        private static (SizeDescription? format, int precision, bool appendUnit) ParseFormat(string formatLower)
        {
            var incorrect = (null as SizeDescription, 0, false);

            if (string.IsNullOrEmpty(formatLower)) return incorrect;

            // Первый символ должен быть одним из поддерживаемых форматов
            char formatChar = formatLower[0];
            var format = SupportedFormats.FirstOrDefault(f => f.FormatChar == formatChar);
            if (format == null) return incorrect;

            int precision = 0;
            bool appendUnit = false;
            int idx = 1;

            // Проверяем наличие числового символа после буквы формата
            if (idx < formatLower.Length)
            {
                if (char.IsDigit(formatLower[idx]))
                {
                    precision = formatLower[idx] - '0';
                    idx++;
                }
            }

            // Проверяем наличие '+'
            if (idx < formatLower.Length)
            {
                if (formatLower[idx] == '+')
                {
                    appendUnit = true;
                    idx++;
                }
                else
                {
                    // Недопустимый символ
                    return incorrect;
                }
            }

            // Не должно быть больше символов
            if (idx < formatLower.Length) return incorrect;

            return (format, precision, appendUnit);
        }

        private static string ConvertSize(long sizeInBytes, int power, string unit, int precision, bool appendUnit)
        {
            long divisor = (long)Math.Pow(1024, power);
            double result = (double)sizeInBytes / divisor;
            string formatted = result.ToString($"F{precision}");
            return appendUnit ? $"{formatted} {unit}" : formatted;
        }

        private static string FormatWithThousandsSeparator(long number)
        {
            var numberFormat = (System.Globalization.NumberFormatInfo)System.Globalization.CultureInfo.CurrentCulture.NumberFormat.Clone();
            numberFormat.NumberGroupSeparator = " ";
            return number.ToString("N0", numberFormat);
        }

        private class SizeDescription
        {
            public SizeDescription(char formatChar, int power, string unit)
            {
                FormatChar = formatChar;
                Power = power;
                Unit = unit;
            }

            public char FormatChar { get; }
            public int Power { get; }
            public string Unit { get; }
        }
    }
}