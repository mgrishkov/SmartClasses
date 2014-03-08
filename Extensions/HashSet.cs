using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Add range of values to the hashset
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="values"></param>
        public static void AddRange<T>(this HashSet<T> s, IEnumerable<T> values)
        {
            foreach (var itm in values)
            {
                s.Add(itm);
            }
            ;
        }
    }
}
