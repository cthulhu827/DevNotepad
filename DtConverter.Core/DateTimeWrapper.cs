using System;

namespace DtConverter
{
    public class DateTimeWrapper : IWrapper
    {
        private readonly DateTimeWrapperFormat format;

        public DateTimeWrapper(long value, DateTimeWrapperFormat format)
        {
            Value = value;
            this.format = format;
        }

        public long Value { get; }

        public DateTimeWrapper ChangeFormat(DateTimeWrapperFormat newFormat)
        {
            return new DateTimeWrapper(Value, newFormat);
        }

        public DateTimeWrapper Add(TimeSpan span)
        {
            return new DateTimeWrapper(Value + (long)span.TotalMilliseconds, format);
        }

        private static string FormatDateTime(DateTime dt)
        {
            var s = dt.ToString("G");
            if (dt.Millisecond != 0) s += "." + dt.Millisecond.ToString("D3");
            return s;
        }

        public override string ToString()
        {
            switch (format)
            {
                case DateTimeWrapperFormat.Utc:
                {
                    var dt = DateTimeOffset.FromUnixTimeMilliseconds(Value).UtcDateTime;
                    return FormatDateTime(dt) + " UTC";
                }
                case DateTimeWrapperFormat.Msk:
                {
                    var dt = DateTimeOffset.FromUnixTimeMilliseconds(Value).UtcDateTime.AddHours(3);
                    return FormatDateTime(dt) + " msk";
                }
                case DateTimeWrapperFormat.Epoch:
                    return (Value / 1000L).ToString();
                case DateTimeWrapperFormat.EpochMs:
                    return Value.ToString();
                case DateTimeWrapperFormat.Pg:
                {
                    var dt = DateTimeOffset.FromUnixTimeMilliseconds(Value).UtcDateTime;
                    var s = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    if (dt.Millisecond != 0)
                        s += "." + dt.Millisecond.ToString("D3");
                    return s;
                }
                case DateTimeWrapperFormat.Json:
                {
                    var dt = DateTimeOffset.FromUnixTimeMilliseconds(Value).UtcDateTime;
                    return dt.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                }
                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, null);
            }
        }
    }
}