using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Merges the source with the "list" using "comparer" function and performs "deleted" action for the elements that should be deleted from the source list, and "inserted" action for the elements that should be added to the source list
        /// </summary>
        /// <returns></returns>
        public static List<T> Merge<T>(this List<T> s, List<T> list, Func<T, T, bool> comparer, Action<T> deleted, Action<T> inserted)
            where T: class
        {
            var sCount = s.Count;
            var lCount = list.Count;

            var toRemove = new List<T>();
            var toAdd = new List<T>();

            for (var i = 0; i < sCount; i++)
            {
                var itm = s[i];
                if (!list.Any(x => comparer(x, itm)))
                {
                    toRemove.Add(itm);
                    deleted(itm);
                }
                ;
            }
            ;

            for (var i = 0; i < lCount; i++)
            {
                var itm = list[i];
                if (!s.Any(x => comparer(x, itm)))
                {
                    toAdd.Add(itm);
                    inserted(itm);
                }
                ;
            }
            ;

            foreach (var i in toRemove)
            {
                s.Remove(i);
            }
            ;
            s.AddRange(toAdd);

            return s;
        }
    }
}
