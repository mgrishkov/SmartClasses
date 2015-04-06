using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace SmartClasses.Utils
{
    public class StringUtils
    {
        public static string FixDecimalSeparator(string value)
        {
            var decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            switch (decimalSeparator)
            {
                case ".":
                    value = value.Replace(",", decimalSeparator);
                    break;
                case ",":
                    value = value.Replace(".", decimalSeparator);
                    break;
            };
            return value;
        }
    }
}
