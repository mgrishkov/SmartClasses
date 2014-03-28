using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static StringBuilder AppendFormatLine(this StringBuilder s, string format, params object[] args)
        {
            return s.AppendFormat(format + Environment.NewLine, args);
        }
    }
}
