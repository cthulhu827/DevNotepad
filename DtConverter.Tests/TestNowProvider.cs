using System;

namespace DtConverter.Tests;

public class TestNowProvider : INowProvider
{
    public DateTime Now() => new DateTime(2026, 5, 4, 18, 55, 0, DateTimeKind.Utc);
}