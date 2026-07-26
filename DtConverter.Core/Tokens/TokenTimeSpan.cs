using System;

namespace DtConverter.Tokens;

public class TokenTimeSpan : Token, IValueStrToken
{
    public static TokenTimeSpan Create(TimeSpan value, string valueStr)
    {
        var tokenType = value >= TimeSpan.FromDays(1) ? TokenType.TimeSpanWithDays : TokenType.TimeSpan;
        return new TokenTimeSpan(tokenType, value, valueStr);
    }

    public static TokenTimeSpan CreateHuman(TimeSpan value)
    {
        return new TokenTimeSpan(TokenType.Human, value, String.Empty);
    }

    private TokenTimeSpan(TokenType tokenType, TimeSpan value, string valueStr)
        : base(tokenType)
    {
        Value = value;
        ValueStr = valueStr;
    }

    public TimeSpan Value { get; }

    public string ValueStr { get; }
}