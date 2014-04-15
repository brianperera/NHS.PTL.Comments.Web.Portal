using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

/// <summary>
/// Summary description for Utility
/// </summary>
namespace Nhs.Ptl.Comments.Utility
{
    public static class DateTimeHelper
    {
        public static DateTime FirstDateInWeek(this DateTime dt)
        {
            while (dt.DayOfWeek != CultureInfo.CurrentUICulture.DateTimeFormat.FirstDayOfWeek)
                dt = dt.AddDays(-1);
            return dt;
        }

    }
}