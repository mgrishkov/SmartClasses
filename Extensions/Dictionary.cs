using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Add or replace item to the dictionary
        /// </summary>
        public static void AddOrRepace<TKey, TValue>(this IDictionary<TKey, TValue> s, TKey key, TValue value)
        {
            if (s.ContainsKey(key))
            {
                s[key] = value;
            }
            else
            {
                s.Add(new KeyValuePair<TKey, TValue>(key, value));
            };
        }

        /// <summary>
        /// Remove item from the dictionary if it exists
        /// </summary>
        /// <returns>True if the item has been deleted</returns>
        public static bool TryRemove<TKey, TValue>(this IDictionary<TKey, TValue> s, TKey key)
        {
            var result = false;
            if (s.ContainsKey(key))
            {
                s.Remove(key);
            };
            return result;
        }

        /// <summary>
        /// Converts Dictionary to dynamic class
        /// </summary>
        public static dynamic ToDynamic<TValue>(this  IDictionary<string, TValue> s)
        {
            var eo = new ExpandoObject();
            var eoColl = (ICollection<KeyValuePair<string, object>>)eo;
            foreach (var itm in s)
            {
                eoColl.Add(new KeyValuePair<string, object>(itm.Key, (object)itm.Value));
            };
            return (dynamic)eo;
        }

        /// <summary>
        /// Add or replace range of items
        /// </summary>
        public static bool AddOrReplaceRange<TKey, TValue>(this IDictionary<TKey, TValue> s, IDictionary<TKey, TValue> items)
        {
            var result = false;
            foreach (var itm in items)
            {
                s.AddOrRepace(itm.Key, itm.Value);
            };
            return result;
        }
    }
}
