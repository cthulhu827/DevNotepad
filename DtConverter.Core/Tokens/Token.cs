namespace DtConverter.Tokens;

public class Token
{
    public Token(TokenType type)
    {
        Type = type;
    }

    public TokenType Type { get; }
}