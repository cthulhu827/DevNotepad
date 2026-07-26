using System;

namespace DtConverter;

public class NowProvider : INowProvider
{
    public DateTime Now() => DateTime.UtcNow;
}