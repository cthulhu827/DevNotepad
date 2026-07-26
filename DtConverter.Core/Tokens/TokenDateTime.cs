using System;

namespace DtConverter.Tokens;

public class TokenDateTime : Token, IValueStrToken
{
    public TokenDateTime(DateTime value, string valueStr)
        : base(value.TimeOfDay == TimeSpan.Zero ? TokenType.Date : TokenType.TimeWithTimeZone)
    {
        Value = value;
        ValueStr = valueStr;
    }

    public DateTime Value { get; }

    public string ValueStr { get; }
}