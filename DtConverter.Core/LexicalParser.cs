using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using DtConverter.Tokens;

namespace DtConverter;

public class LexicalParser
{
    private readonly INowProvider nowProvider;

    public LexicalParser(INowProvider nowProvider)
    {
        this.nowProvider = nowProvider;
    }

    public IWrapper? Parse(IWrapper? prev, string line)
    {
        return Parse(prev, line, out _);
    }

    private Token[] GetTokens(string line)
    {
        var commentStart = line.IndexOf("--");
        if (commentStart != -1) line = line.Substring(0, commentStart);

        line = line.Trim();

        if (string.IsNullOrWhiteSpace(line)) return Array.Empty<Token>();

        var tokensStr = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var result = new List<Token>();
        foreach (var token in tokensStr)
        {
            var parsed = ParseToken(token);
            if (parsed != null)
                result.Add(parsed);
            else
                throw new Exception($"Unknown token {token}");
        }

        return result.ToArray();
    }

    public IWrapper? Parse(IWrapper? prev, string line, out Token[] tokens)
    {
        tokens = GetTokens(line);
        if (!tokens.Any()) return new EmptyWrapper();

        var (unprocessed, modifierTokens) = GetModifiers(tokens);
        var (dateTokens, unprocessed2) = GetDate(unprocessed);
        var additionTokens = GetAdditions(unprocessed2);

        ApplyPrevIfNeed(prev, ref dateTokens, ref additionTokens);

        IWrapper? result = GetDateTimeWrapper2(dateTokens, additionTokens, modifierTokens);
        return result ?? GetTimeWrapper2(additionTokens, modifierTokens);
    }

    private void ApplyPrevIfNeed(IWrapper? prev, ref Token[] dateTokens,
        ref Token[] additionTokens)
    {
        if (prev == null) return;

        // Если задаётся дата, то это оверрайд prev'а, поэтому его игнорируем.
        if (dateTokens.Any()) return;

        // Есть только модификаторы - нужно применить их к prev.
        if (!dateTokens.Any() && !additionTokens.Any())
        {
            if (prev is DateTimeWrapper dateTimeWrapper)
            {
                dateTokens = new Token[] { new TokenInt(dateTimeWrapper.Value) };
                return;
            }

            if (prev is TimeWrapper timeWrapper)
            {
                additionTokens = new Token[] { TokenTimeSpan.CreateHuman(timeWrapper.Value) };
                return;
            }

            return;
        }

        // Если в строке время, и строка начинается с плюса, то это не новое значение
        // времени, а добавление к предыдущему.
        var firstToken = additionTokens.FirstOrDefault()?.Type;
        if (firstToken is TokenType.Plus or TokenType.Minus or TokenType.Trim)
        {
            if (prev is DateTimeWrapper dateTimeWrapper)
            {
                dateTokens = new Token[] { new TokenInt(dateTimeWrapper.Value) };
                return;
            }

            if (prev is TimeWrapper timeWrapper)
            {
                // todo: криво
                var tmp = new List<Token> { TokenTimeSpan.CreateHuman(timeWrapper.Value) };
                tmp.AddRange(additionTokens);
                additionTokens = tmp.ToArray();
            }
        }
    }

    private TimeWrapper? GetTimeWrapper2(Token[] additionTokens, Token[] modifierTokens)
    {
        var result = GetTimeWrapper(additionTokens);

        if (result != null)
        {
            foreach (var token in modifierTokens)
            {
                var format = ModifierToFormat2(((TokenModifier)token).Value);
                result = result.ChangeFormat(format);
            }
        }

        return result;
    }

    private TimeWrapper? GetTimeWrapper(Token[] tokens)
    {
        var (timeSpan, _) = SumTimeTokens(tokens, true);
        return timeSpan == TimeSpan.Zero
            ? null
            : new TimeWrapper(timeSpan, TimeWrapperFormat.Human);
    }

    private DateTimeWrapperFormat ModifierToFormat(Modifier modifier)
    {
        switch (modifier)
        {
            case Modifier.Utc:
                return DateTimeWrapperFormat.Utc;
            case Modifier.Msk:
                return DateTimeWrapperFormat.Msk;
            case Modifier.E:
                return DateTimeWrapperFormat.Epoch;
            case Modifier.Ee:
                return DateTimeWrapperFormat.EpochMs;
            case Modifier.Pg:
                return DateTimeWrapperFormat.Pg;
            case Modifier.Js:
                return DateTimeWrapperFormat.Json;
            default:
                throw new ArgumentOutOfRangeException(nameof(modifier), modifier, null);
        }
    }

    private TimeWrapperFormat ModifierToFormat2(Modifier modifier)
    {
        switch (modifier)
        {
            case Modifier.Ts:
                return TimeWrapperFormat.TimeSpan;
            case Modifier.Hms:
                return TimeWrapperFormat.Human;
            case Modifier.M:
                return TimeWrapperFormat.Minutes;
            case Modifier.S:
                return TimeWrapperFormat.Seconds;
            case Modifier.Ms:
                return TimeWrapperFormat.Milliseconds;
            default:
                throw new ArgumentOutOfRangeException(nameof(modifier), modifier, null);
        }
    }

    private (TimeSpan, bool) SumTimeTokens(Token[] tokens, bool ignoreTrim)
    {
        var result = TimeSpan.Zero;
        var needTrim = false;
        if (!tokens.Any()) return (result, false);

        var add = true;
        foreach (var token in tokens)
        {
            if (token.Type == TokenType.Plus)
                add = true;
            else if (token.Type == TokenType.Minus)
                add = false;
            else if (token is TokenTimeSpan timeToken)
            {
                var op = add ? 1 : -1;
                result = result.Add(op * timeToken.Value);
            }
            else if (token.Type == TokenType.Trim)
            {
                if (!ignoreTrim)
                {
                    result = TimeSpan.Zero;
                    needTrim = true;
                }
            }
            else
                throw new ArgumentOutOfRangeException($"Unexpected token {token.Type}");
        }

        return (result, needTrim);
    }

    private DateTimeWrapper? GetDateTimeWrapper2(Token[] dateTokens, Token[] additionTokens, Token[] modifierTokens)
    {
        var dateTime = GetDateTimeWrapper(dateTokens);
        if (dateTime != null)
        {
            var (timeSpanToAdd, needTrim) = SumTimeTokens(additionTokens, false);
            if (needTrim) dateTime = dateTime.Trim();
            dateTime = dateTime.Add(timeSpanToAdd);

            foreach (var token in modifierTokens)
            {
                var format = ModifierToFormat(((TokenModifier)token).Value);
                dateTime = dateTime.ChangeFormat(format);
            }
        }

        return dateTime;
    }

    private DateTimeWrapper? GetDateTimeWrapper(Token[] dateTokens)
    {
        if (dateTokens.Length == 1)
        {
            var token = dateTokens.Single();
            if (token.Type == TokenType.Int)
            {
                var epochValue = ((TokenInt)token).Value;
                // Timestamps >= 10^12 are in milliseconds, smaller ones are in seconds
                long ms = epochValue >= 1_000_000_000_000L
                    ? epochValue
                    : epochValue * 1000L;

                return new DateTimeWrapper(ms, DateTimeWrapperFormat.Utc);
            }

            if (token.Type == TokenType.Date)
            {
                var dt = ((TokenDateTime)token).Value;
                long ms = new DateTimeOffset(dt, TimeSpan.Zero).ToUnixTimeMilliseconds();
                return new DateTimeWrapper(ms, DateTimeWrapperFormat.Utc);
            }

            if (token.Type == TokenType.Now)
            {
                long ms = new DateTimeOffset(nowProvider.Now(), TimeSpan.Zero).ToUnixTimeMilliseconds();
                return new DateTimeWrapper(ms, DateTimeWrapperFormat.Utc);
            }

            if (token.Type == TokenType.TimeWithTimeZone)
            {
                return ParseDateTime(((IValueStrToken)token).ValueStr);
            }

            return null;
        }

        if (dateTokens.Length == 2)
        {
            var str = string.Join(' ', dateTokens.Cast<IValueStrToken>().Select(t => t.ValueStr));
            return ParseDateTime(str);
        }

        return null;
    }

    public static DateTimeWrapper? ParseDateTime(string input)
    {
        if (DateTime.TryParse(input, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out DateTime dt))
        {
            long ms = new DateTimeOffset(dt, TimeSpan.Zero).ToUnixTimeMilliseconds();
            return new DateTimeWrapper(ms, DateTimeWrapperFormat.Utc);
        }

        return null;
    }

    private Token? ParseToken(string token)
    {
        if (long.TryParse(token, out var intValue)) return new TokenInt(intValue);
        if (TimeSpan.TryParse(token, out var timeSpan)) return TokenTimeSpan.Create(timeSpan, token);

        if (DateTime.TryParse(token, CultureInfo.CurrentCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var dateTime))
            return new TokenDateTime(dateTime, token);

        if (TryParseModifier(token, out var modifier)) return new TokenModifier(modifier);

        if (token == "+") return new Token(TokenType.Plus);
        if (token == "-") return new Token(TokenType.Minus);
        if (token == "now") return new Token(TokenType.Now);
        if (token == "trim") return new Token(TokenType.Trim);

        if (TryParseHuman(token, out var human)) return TokenTimeSpan.CreateHuman(human);

        return null;
    }

    private bool TryParseModifier(string token, out Modifier modifier)
    {
        return Enum.TryParse(token, ignoreCase: true, out modifier) &&
            Enum.IsDefined(typeof(Modifier), modifier);
    }

    private static readonly Regex HumanTokenRegex = new Regex(@"^(\d+)(ms|d|h|m|s)$", RegexOptions.Compiled);

    private bool TryParseHuman(string token, out TimeSpan timeSpan)
    {
        timeSpan = TimeSpan.Zero;

        var match = HumanTokenRegex.Match(token);
        if (!match.Success) return false;
        if (!int.TryParse(match.Groups[1].Value, out int value)) return false;

        timeSpan = match.Groups[2].Value switch
        {
            "d" => TimeSpan.FromDays(value),
            "h" => TimeSpan.FromHours(value),
            "m" => TimeSpan.FromMinutes(value),
            "s" => TimeSpan.FromSeconds(value),
            "ms" => TimeSpan.FromMilliseconds(value),
            _ => TimeSpan.Zero
        };

        return timeSpan != TimeSpan.Zero;
    }

    private (Token[] unprocessed, Token[] modifiers) GetModifiers(
        IReadOnlyCollection<Token> tokens)
    {
        var modifiers = tokens
            .Reverse()
            .TakeWhile(t => t.Type == TokenType.Modifier)
            .Reverse()
            .ToArray();
        var unprocessed = tokens
            .Take(tokens.Count - modifiers.Length)
            .ToArray();

        if (unprocessed.Any(t => t.Type == TokenType.Modifier))
            throw new Exception("Modifiers order");

        return (unprocessed, modifiers);
    }

    private (Token[] date, Token[] unprocessed) GetDate(
        IReadOnlyCollection<Token> tokens)
    {
        if (!tokens.Any()) return (Array.Empty<Token>(), Array.Empty<Token>());

        int dateTokenCount = 0;

        var firstToken = tokens.First();
        if (firstToken.Type is TokenType.Now or TokenType.Int or TokenType.TimeWithTimeZone)
        {
            dateTokenCount = 1;
        }
        else if (firstToken.Type == TokenType.Date)
        {
            if (tokens.Count == 1)
                dateTokenCount = 1;
            else
            {
                var secondToken = tokens.Skip(1).First();
                dateTokenCount = secondToken.Type is TokenType.TimeSpan or TokenType.TimeWithTimeZone ? 2 : 1;
            }
        }

        var unprocessed = tokens.Skip(dateTokenCount).ToArray();
        CheckNoDateTokens(unprocessed);
        return (tokens.Take(dateTokenCount).ToArray(), unprocessed);
    }

    private Token[] GetAdditions(IReadOnlyCollection<Token> tokens)
    {
        var additionTokens = new[]
        {
            TokenType.TimeSpan,
            TokenType.TimeSpanWithDays,
            TokenType.Human,
            TokenType.Plus,
            TokenType.Minus,
            TokenType.Trim
        };
        return tokens.All(t => additionTokens.Contains(t.Type))
            ? tokens.ToArray()
            : throw new Exception("Incorrect additions");
    }

    private void CheckNoDateTokens(IEnumerable<Token> tokens)
    {
        var dateTokens = new[]
        {
            TokenType.Now,
            TokenType.Int,
            TokenType.TimeWithTimeZone,
            TokenType.Date
        };
        if (tokens.Any(t => dateTokens.Contains(t.Type)))
            throw new Exception("Date order");
    }
}