using System.Globalization;
using System.Reflection;
using DtConverter.Tokens;
using TT = DtConverter.Tokens.TokenType;

namespace DtConverter.Tests;

[TestFixture]
public class DateTimeWrapperTests
{
    [SetUp]
    public void SetRussianCulture()
    {
        var culture = (CultureInfo)CultureInfo.GetCultureInfo("ru-RU").Clone();
        culture.DateTimeFormat.ShortDatePattern = "dd.MM.yyyy";
        culture.DateTimeFormat.LongTimePattern = "H:mm:ss";
        culture.DateTimeFormat.ShortTimePattern = "H:mm";
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
    }

    // @formatter:off
    [TestCase("01.05.2026 16:58:47", "01.05.2026 16:58:47 UTC", TT.Date, TT.TimeSpan)]
    [TestCase("01.05.2026 16:58:47.456", "01.05.2026 16:58:47.456 UTC", TT.Date, TT.TimeSpan)]
    [TestCase("1777654727", "01.05.2026 16:58:47 UTC", TT.Int)]
    [TestCase("1777654727123", "01.05.2026 16:58:47.123 UTC", TT.Int)]
    [TestCase("01.05.2026 16:58:47.123 ee", "1777654727123", TT.Date, TT.TimeSpan, TT.Modifier)]
    [TestCase("01.05.2026 16:58:47.123 e", "1777654727", TT.Date, TT.TimeSpan, TT.Modifier)]
    [TestCase("01.05.2026 16:58:47.123 e ee", "1777654727123", TT.Date, TT.TimeSpan, TT.Modifier, TT.Modifier)]
    [TestCase("01.05.2026 16:58:47.123 msk", "01.05.2026 19:58:47.123 msk", TT.Date, TT.TimeSpan, TT.Modifier)]
    [TestCase("01.05.2026 16:58:47.123 utc", "01.05.2026 16:58:47.123 UTC", TT.Date, TT.TimeSpan, TT.Modifier)]
    [TestCase("1777654727123 msk", "01.05.2026 19:58:47.123 msk", TT.Int, TT.Modifier)]
    [TestCase("01.05.2026 16:58:47.123 pg", "2026-05-01 16:58:47.123", TT.Date, TT.TimeSpan, TT.Modifier)]
    [TestCase("01.05.2026", "01.05.2026 0:00:00 UTC", TT.Date)]
    [TestCase("01.05.2026 ee", "1777593600000", TT.Date, TT.Modifier)]
    [TestCase("01.05.2026 msk", "01.05.2026 3:00:00 msk", TT.Date, TT.Modifier)]
    [TestCase("01.05.2026 19:58:47+3", "01.05.2026 16:58:47 UTC", TT.Date, TT.TimeWithTimeZone)]
    [TestCase("01.05.2026 19:58:47-3", "01.05.2026 22:58:47 UTC", TT.Date, TT.TimeWithTimeZone)]
    [TestCase("01.05.2026 19:58:47.256+3 ee", "1777654727256", TT.Date, TT.TimeWithTimeZone, TT.Modifier)]
    [TestCase("01.05.2026 19:58:47+3 msk", "01.05.2026 19:58:47 msk", TT.Date, TT.TimeWithTimeZone, TT.Modifier)]

    [TestCase("02:03:01", "2h 3m 1s", TT.TimeSpan)]
    [TestCase("02:00:01", "2h 1s", TT.TimeSpan)]
    [TestCase("4.02:00:01.583", "4d 2h 1s 583ms", TT.TimeSpanWithDays)]
    [TestCase("2h 3m 1s", "2h 3m 1s", TT.Human, TT.Human, TT.Human)]
    [TestCase("2h 1s", "2h 1s", TT.Human, TT.Human)]
    [TestCase("+ 2h 1s", "2h 1s", TT.Plus, TT.Human, TT.Human)]
    [TestCase("4d 2h 1s 583ms", "4d 2h 1s 583ms", TT.Human, TT.Human, TT.Human, TT.Human)]

    [TestCase("4.02:00:01.583 ts", "4.02:00:01.583", TT.TimeSpanWithDays, TT.Modifier)]
    [TestCase("4d 2h 1s 583ms ts", "4.02:00:01.583", TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("4.02:00:01.583 hms", "4d 2h 1s 583ms", TT.TimeSpanWithDays, TT.Modifier)]
    [TestCase("4d 2h 1s 583ms hms", "4d 2h 1s 583ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("4d 2h 1s 583ms ts hms", "4d 2h 1s 583ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier, TT.Modifier)]

    [TestCase("01.05.2026 17:23:01 + 2m msk", "01.05.2026 20:25:01 msk", TT.Date, TT.TimeSpan, TT.Plus, TT.Human, TT.Modifier)]
    [TestCase("2h 5m + 3h 10m ts", "05:15:00", TT.Human, TT.Human, TT.Plus, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("2h 5m + 03:10:00 ts", "05:15:00", TT.Human, TT.Human, TT.Plus, TT.TimeSpan, TT.Modifier)]
    [TestCase("2h 5m + 1.03:10:00 ts", "1.05:15:00", TT.Human, TT.Human, TT.Plus, TT.TimeSpanWithDays, TT.Modifier)]
    [TestCase("01.05.2026 17:23:01 + 1.01:02:03 msk", "02.05.2026 21:25:04 msk", TT.Date, TT.TimeSpan, TT.Plus, TT.TimeSpanWithDays, TT.Modifier)]

    [TestCase("now + 2m msk", "04.05.2026 21:57:00 msk", TT.Now, TT.Plus, TT.Human, TT.Modifier)]

    [TestCase("1d 3h 41m 32s 16ms", "1d 3h 41m 32s 16ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Human)]
    [TestCase("1d 3h 41m 32s 16ms m", "1661m 32s 16ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("1d 3h 41m 32s 16ms s", "99692s 16ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("1d 3h 41m 32s 16ms ms", "99692016ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier)]
    [TestCase("1d 3h 41m 32s 16ms m ms s", "99692s 16ms", TT.Human, TT.Human, TT.Human, TT.Human, TT.Human, TT.Modifier, TT.Modifier, TT.Modifier)]
    [TestCase("99692s 16ms hms", "1d 3h 41m 32s 16ms", TT.Human, TT.Human, TT.Modifier)]

    [TestCase("18:37 - 16:40", "1h 57m", TT.TimeSpan, TT.Minus, TT.TimeSpan)]
    [TestCase("now - 2m msk", "04.05.2026 21:53:00 msk", TT.Now, TT.Minus, TT.Human, TT.Modifier)]
    [TestCase("18:37 - 1h 7m + 2h 20m 10m ts", "20:00:00", TT.TimeSpan, TT.Minus, TT.Human, TT.Human, TT.Plus, TT.Human, TT.Human, TT.Human, TT.Modifier)]

    [TestCase("-- Full line comment", "")]
    [TestCase("1d 3h -- Part line comment", "1d 3h", TT.Human, TT.Human)]
    [TestCase("  1d   4h  15m ", "1d 4h 15m", TT.Human, TT.Human, TT.Human)]

    [TestCase("01.05.2026 16:58:47.123 trim", "01.05.2026 0:00:00 UTC", TT.Date, TT.TimeSpan, TT.Trim)]
    [TestCase("01.05.2026 00:00:00 trim", "01.05.2026 0:00:00 UTC", TT.Date, TT.TimeSpan, TT.Trim)]
    [TestCase("01.05.2026 16:58:47.123 + 2h - 3m trim + 1h + 15m", "01.05.2026 1:15:00 UTC",
        TT.Date, TT.TimeSpan, TT.Plus, TT.Human, TT.Minus, TT.Human, TT.Trim, TT.Plus, TT.Human, TT.Plus, TT.Human)]
    [TestCase("1d 4h trim 15m", "1d 4h 15m", TT.Human, TT.Human, TT.Trim, TT.Human)]

    // @formatter:on
    public void TryParse_EpochSeconds_ReturnsCorrectUtcString(string inputString, string expected,
        params TokenType[] expectedTokens)
    {
        var actual2 = new LexicalParser(new TestNowProvider()).Parse(null, inputString, out var tokens);

        TT[] actualTokens = tokens.Select(t => t.Type).ToArray();
        AssertTokens(actualTokens, expectedTokens);

        Assert.AreEqual(expected, actual2.ToString());
    }

    /// <summary>
    /// "11:00:00+3" интерпретируется как "11 утра в часовом поясе UTC+3 (= 8 утра UTC) текущего дня".
    /// </summary>
    [Test]
    public void TestParseTimeWithTimeZone()
    {
        const string inputString = "11:00:00+3";

        var actual = new LexicalParser(new TestNowProvider()).Parse(null, inputString, out var tokens);
        var actualTokens = tokens.Select(t => t.Type).ToArray();
        AssertTokens(actualTokens, TT.TimeWithTimeZone);
        Assert.IsTrue(actual is DateTimeWrapper);

        var actualValue = ((DateTimeWrapper)actual).Value;
        var expectedValue = new DateTimeOffset(DateTime.UtcNow.Date.AddHours(8)).ToUnixTimeMilliseconds();
        Assert.AreEqual(expectedValue, actualValue);
    }

    [TestCaseSource(nameof(GetTestCasesPrev))]
    public void TestPrev(string resourceName)
    {
        var (inLines, expectedOutLines) = ReadTwoLists(resourceName);

        IWrapper? prev = null;
        for (int i = 0; i < inLines.Length; i++)
        {
            var actual = new LexicalParser(new TestNowProvider()).Parse(prev, inLines[i]);
            Assert.AreEqual(expectedOutLines[i], actual?.ToString());
            prev = actual;
        }
    }

    private void AssertTokens(TokenType[] actualTokens, params TokenType[] expectedTokens)
    {
        Assert.AreEqual(expectedTokens.Length, actualTokens.Length);
        foreach (var (actualToken, expectedToken) in actualTokens.Zip(expectedTokens))
        {
            Assert.AreEqual(expectedToken, actualToken);
        }
    }

    private static IEnumerable<TestCaseData> GetTestCasesPrev()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var prefix = "DtConverter.Tests.TestCasesPrev.";

        foreach (var resourceName in assembly.GetManifestResourceNames())
        {
            if (!resourceName.StartsWith(prefix) || !resourceName.EndsWith(".txt"))
                continue;

            // SetName только для читаемого отображения в Test Explorer
            var shortName = resourceName[prefix.Length..];
            yield return new TestCaseData(resourceName).SetName(shortName);
        }
    }

    private static string[] ReadResource(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using var stream = assembly.GetManifestResourceStream(resourceName);
        using var reader = new StreamReader(stream!);
        var lines = new List<string>();
        while (reader.ReadLine() is { } line) lines.Add(line);
        return lines.ToArray();
    }

    private static (string[], string[]) ReadTwoLists(string resourceName)
    {
        var allLines = ReadResource(resourceName);

        var list1 = new List<string>();
        var list2 = new List<string>();
        var separatorFound = false;

        foreach (var line in allLines)
        {
            if (string.IsNullOrEmpty(line)) continue;

            if (!separatorFound && line.All(c => c == '-'))
            {
                separatorFound = true;
                continue;
            }

            if (separatorFound)
                list2.Add(line);
            else
                list1.Add(line);
        }

        return (list1.ToArray(), list2.ToArray());
    }
}