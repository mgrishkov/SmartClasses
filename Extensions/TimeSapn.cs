using System;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static DateTime ToDateTime(this TimeSpan s)
        {
            return DateTime.Now.Date.Add(s);
        }
    }
}