using System;
using System.Collections.Generic;
using System.Text;

namespace PlannerModel.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime LastMonth(this DateTime date) => date.AddMonths(-1);
        public static DateTime FirstOfMonth(this DateTime date) => date.AddDays(-date.Day + 1);
    }
}
