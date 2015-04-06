using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Providers
{
    public class FixedSignFormatProvider : IFormatProvider, ICustomFormatter
    {
        public object GetFormat(Type formatType)
        {
            if (formatType == typeof(ICustomFormatter))
                return this;
            else
                return null;
        }

        public string Format(string format, object arg, IFormatProvider formatProvider)
        {
            var result = String.Empty;

            // Check whether this is an appropriate callback              
            if (!this.Equals(formatProvider))
                return null;

            var length = 0;
            // Set default format specifier              
            if (string.IsNullOrEmpty(format))
            {
                format = "F3";
                length = 3;
            }
            else
            {
                var match = Regex.Match(format, @"^[A-Z](?<length>\d+)");
                if (!match.Success)
                    throw new ArgumentOutOfRangeException("format");

                length = Int32.Parse(match.Groups["length"].Value);
            }
                

            string numericString = ((decimal)arg).ToString(format);

            result = numericString.Substring(0, length + 1);

            //remove last symbol if it is NumberDecimalSeparator
            if (result[length].ToString() == CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator)
            {
                return result.Remove(length);
            }

            return result;
        }

    }
}
