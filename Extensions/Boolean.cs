using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static string ToSAPBoolean(this bool s)
        {
            return (s) ? "X" : " ";
        }
    }
}
