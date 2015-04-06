using System;
using System.Linq;
using SmartClasses.Extensions;

namespace HPB.Common.Utils
{
    public class DateTimeUtils
    {
        public static DateTime GetStartOfQuarter(int quarter, int year)
        {
            var month = 3 * (quarter - 1) + 1;
            return new DateTime(year, month, 1);
        }
        public static DateTime GetStartOfQuarter(int quarter)
        {
            var month = 3 * (quarter - 1) + 1;
            return new DateTime(DateTime.Now.Year, month, 1);
        }

        public static DateTime GetEndOfQuarter(int quarter, int year)
        {
            return DateTimeUtils.GetStartOfQuarter(quarter, year).AddMonths(3).AddMinutes(-1);
        }
        public static DateTime GetEndOfQuarter(int quarter)
        {
            return DateTimeUtils.GetStartOfQuarter(quarter, DateTime.Now.Year).AddMonths(3).AddMinutes(-1);
        }

        public static DateTime GetStartOfYear(int year)
        {
            return new DateTime(year, 1, 1);
        }
        public static DateTime GetEndOfYear(int year)
        {
            return new DateTime(year, 12, 31);
        }

        public static Int32 GetQuarterNumber(int month)
        {
            if (month.In(1, 2, 3))
                return 1;
            else if (month.In(4, 5, 6))
                return 2;
            else if (month.In(7, 8, 9))
                return 3;
            else
                return 4;
        }

        public static DateTime? Max(params DateTime?[] args)
        {
            return args.ToList()
                .Where(x => x.HasValue)
                .OrderByDescending(x => x)
                .FirstOrDefault();
        }
    }
}
