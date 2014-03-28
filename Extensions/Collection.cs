using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static ICollection<T> Replace<T>(this ICollection<T> src, IEnumerable<T> dest)
        {
            src.Clear();
            foreach(var itm in dest)
            {
                src.Add(itm);
            };
            return src;
        }
    }
}