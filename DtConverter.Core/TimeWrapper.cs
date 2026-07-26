using System;
using System.Collections.Generic;

namespace DtConverter;

public class TimeWrapper : IWrapper
{
    private readonly TimeWrapperFormat format;

    public TimeWrapper(TimeSpan value, TimeWrapperFormat format)
    {
        Value = value;
        this.format = format;
    }

    public TimeSpan Value { get; }

    public TimeWrapper ChangeFormat(TimeWrapperFormat newFormat)
    {
        return new TimeWrapper(Value, newFormat);
    }

    private static string FormatTimeSpan(TimeSpan ts)
    {
        long subSecondTicks = ts.Ticks % TimeSpan.TicksPerSecond;
        if (ts.Days == 0)
            return subSecondTicks == 0
                ? ts.ToString(@"hh\:mm\:ss")
                : ts.ToString(@"hh\:mm\:ss\.FFFFFFF");
        return subSecondTicks == 0
            ? ts.ToString(@"d\.hh\:mm\:ss")
            : ts.ToString(@"d\.hh\:mm\:ss\.FFFFFFF");
    }

    private string ToHumanString()
    {
        var ts = Value;
        var parts = new List<string>();

        if (ts.Days != 0) parts.Add(ts.Days + "d");
        if (ts.Hours != 0) parts.Add(ts.Hours + "h");
        if (ts.Minutes != 0) parts.Add(ts.Minutes + "m");
        if (ts.Seconds != 0) parts.Add(ts.Seconds + "s");
        if (ts.Milliseconds != 0) parts.Add(ts.Milliseconds + "ms");

        return parts.Count > 0 ? string.Join(" ", parts) : "0s";
    }

    private string ToMinutesString()
    {
        var ts = Value;
        var parts = new List<string>();

        var totalMinutes = (long)ts.TotalMinutes;
        if (totalMinutes != 0) parts.Add(totalMinutes + "m");

        if (ts.Seconds != 0) parts.Add(ts.Seconds + "s");
        if (ts.Milliseconds != 0) parts.Add(ts.Milliseconds + "ms");

        return parts.Count > 0 ? string.Join(" ", parts) : "0s";
    }

    private string ToSecondsString()
    {
        var ts = Value;
        var parts = new List<string>();

        var totalSeconds = (long)ts.TotalSeconds;
        if (totalSeconds != 0) parts.Add(totalSeconds + "s");

        if (ts.Milliseconds != 0) parts.Add(ts.Milliseconds + "ms");

        return parts.Count > 0 ? string.Join(" ", parts) : "0s";
    }

    private string ToMillisecondsString()
    {
        var ts = Value;
        var parts = new List<string>();

        var totalMilliseconds = (long)ts.TotalMilliseconds;
        if (totalMilliseconds != 0) parts.Add(totalMilliseconds + "ms");

        return parts.Count > 0 ? string.Join(" ", parts) : "0ms";
    }

    public override string ToString()
    {
        return format switch
        {
            TimeWrapperFormat.Human => ToHumanString(),
            TimeWrapperFormat.Minutes => ToMinutesString(),
            TimeWrapperFormat.Seconds => ToSecondsString(),
            TimeWrapperFormat.Milliseconds => ToMillisecondsString(),
            TimeWrapperFormat.TimeSpan => FormatTimeSpan(Value),
            _ => throw new ArgumentOutOfRangeException(nameof(format), format, null)
        };
    }
}