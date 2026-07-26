namespace DtConverter.Tokens;

public class TokenModifier : Token
{
    public TokenModifier(Modifier value)
        : base(TokenType.Modifier)
    {
        Value = value;
    }

    public Modifier Value { get; }
}