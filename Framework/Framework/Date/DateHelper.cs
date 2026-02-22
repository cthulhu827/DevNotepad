using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Framework.Date;

namespace System
{
    public static class DateHelper
    {
        public static DayOfWeek ToDayOfWeek(this RusDayOfWeek rusDayOfWeek)
        {
            return rusDayOfWeek == RusDayOfWeek.Sunday
                ? DayOfWeek.Sunday
                : (DayOfWeek)((int)rusDayOfWeek + 1);
        }

        public static RusDayOfWeek ToRusDayOfWeek(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek == DayOfWeek.Sunday
                ? RusDayOfWeek.Sunday
                : (RusDayOfWeek)((int)dayOfWeek - 1);
        }

        public static string DayName(this RusDayOfWeek rusDayOfWeek)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.DayNames[(int)rusDayOfWeek.ToDayOfWeek()];
        }

        public static string ShortDayName(this RusDayOfWeek rusDayOfWeek)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.ShortestDayNames[(int)rusDayOfWeek.ToDayOfWeek()];
        }

        public static string DayName(this DateTime dateTime)
        {
            return dateTime.DayOfWeek.ToRusDayOfWeek().DayName();
        }

        public static string ShortDayName(this DateTime dateTime)
        {
            return dateTime.DayOfWeek.ToRusDayOfWeek().ShortDayName();
        }
    }
}
