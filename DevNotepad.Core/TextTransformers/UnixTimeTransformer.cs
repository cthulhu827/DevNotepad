using System;

namespace DevNotepad.Core.TextTransformers
{
    [TextTransformer("Unix time", "37b9029f-076a-4570-996f-a26a022dbff3")]
    public class UnixTimeTransformer : LineTransformer
    {
        protected override string TransformLine(string line)
        {
            // Попытка распарсить как число (unix timestamp)
            if (long.TryParse(line.Trim(), out long timestamp))
            {
                try
                {
                    DateTimeOffset dateTime;
                    string format;
                    
                    // Определяем, секунды или миллисекунды
                    // Timestamp в секундах имеет ~10 цифр, в миллисекундах ~13 цифр
                    // Используем порог: если больше 10^11, то это миллисекунды
                    if (timestamp > 100000000000) // 10^11
                    {
                        dateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        format = "yyyy-MM-dd HH:mm:ss.fff";
                    }
                    else
                    {
                        dateTime = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        format = "yyyy-MM-dd HH:mm:ss";
                    }
                    
                    return dateTime.ToString(format);
                }
                catch
                {
                    return "error";
                }
            }
            
            // Попытка распарсить как DateTime
            if (DateTime.TryParse(line.Trim(), out DateTime dt))
            {
                try
                {
                    // Интерпретируем как UTC и преобразуем в unix timestamp
                    var dtUtc = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                    var unixTime = new DateTimeOffset(dtUtc).ToUnixTimeSeconds();
                    return unixTime.ToString();
                }
                catch
                {
                    return "error";
                }
            }
            
            return "error";
        }
    }
}