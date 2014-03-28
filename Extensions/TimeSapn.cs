using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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