using System.Collections.Generic;
using System.Linq;

namespace SmartClasses.Extensions
{
    public partial class ExtensionMethods
    {
        public static bool In<T>(this T s, params T[] args) where T : struct
        {
            return args.Contains(s);
        }
        public static bool In<T>(this T s, IEnumerable<T> args) where T : struct
        {
            return args.Contains(s);
        }
    }
}
