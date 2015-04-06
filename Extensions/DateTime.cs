using System;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static String ToSqlDateString(this DateTime date)
        {
            return String.Format("{0:yyyyMMdd}", date);
        }

        public static String ToSqlTimeString(this DateTime date)
        {
            return date.ToString("HH:mm:ss");
        }
        
        public static string ToJSONString(this DateTime date)
        {
            var unixDate = new DateTime(1970, 1, 1);
            var ts = new TimeSpan(date.Ticks - unixDate.Ticks);
            return String.Format(@"/Date({0})/", (long)ts.TotalMilliseconds);
        }

        public static DateTime EndOfMonth(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var daysInMonth = DateTime.DaysInMonth(sender.Year, month);
            return new DateTime(year, month, daysInMonth, 23, 59, 59);
        }
        public static DateTime StartOfMonth(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            return new DateTime(year, month, 1, 00, 00, 00);
        }
        public static DateTime EndOfDay(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            return new DateTime(year, month, day, 23, 59, 59);
        }
        public static DateTime StartOfDay(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            return new DateTime(year, month, day, 00, 00, 00);
        }
        public static DateTime EndOfHour(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            var hour = sender.Hour;
            return new DateTime(year, month, day, hour, 59, 59);
        }
        public static DateTime StartOfHour(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            var hour = sender.Hour;
            return new DateTime(year, month, day, hour, 00, 00);
        }
        public static DateTime EndOfMinute(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            var hour = sender.Hour;
            var minute = sender.Minute;
            return new DateTime(year, month, day, hour, minute, 59);
        }
        public static DateTime StartOfMinute(this DateTime sender)
        {
            var month = sender.Month;
            var year = sender.Year;
            var day = sender.Day;
            var hour = sender.Hour;
            var minute = sender.Minute;
            return new DateTime(year, month, day, hour, minute, 00);
        }
        /// <summary>
        /// Trim datetime to a specific pattern
        /// </summary>
        /// <param name="date"></param>
        /// <param name="roundTicks"></param>
        /// <returns></returns>
        /// <example>DateTime.Now.Trim(TimeSpan.TicksPerDay) - trim to day</example>
        public static DateTime Trim(this DateTime date, long roundTicks)
        {
            return new DateTime(date.Ticks - date.Ticks % roundTicks);
        }
        
        public static Int32 Quarter(this DateTime sender)
        {
            var month = sender.Month;
            if (month.In(1, 2, 3))
                return 1;
            else if (month.In(4, 5, 6))
                return 2;
            else if (month.In(7, 8, 9))
                return 3;
            else
                return 4;
        }        
    }
}
