namespace DtConverter.Tokens;

public class TokenInt : Token
{
    public TokenInt(long value)
        : base(TokenType.Int)
    {
        Value = value;
    }

    public long Value { get; }
}