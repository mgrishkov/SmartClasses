using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartClasses.Attributes;

namespace SmartClasses.Utils
{
    public class EnumUtils
    {
        /// <summary>
        /// Returns dictionary of key and localized text for enum
        /// </summary>
        public static IDictionary<int, string> ToLocalizedDictionary<T>() where T : struct
        {
            var result = new Dictionary<int, string>();
            Type t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                LocalizedNameAttribute[] attr = (LocalizedNameAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(LocalizedNameAttribute), false);
                var text = (attr.Length > 0) ? attr[0].DisplayName : string.Empty;
                result.Add(key, text);
            }
            return result;
        }
        /// <summary>
        /// Returns dictionary of key and description text for enum
        /// </summary>
        public static IDictionary<int, string> ToDescriptionDictionary<T>() where T : struct
        {
            var result = new Dictionary<int, string>();
            Type t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                DescriptionAttribute[] attr = (DescriptionAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(DescriptionAttribute), false);
                var text = (attr.Length > 0) ? attr[0].Description : string.Empty;
                result.Add(key, text);
            }
            return result;
        }
        /// <summary>
        /// Returns localized text for enum
        /// </summary>
        public static string GetLocalizedText<T>() where T : struct
        {
            Type t = typeof(T);
            LocalizedNameAttribute[] attributes = (LocalizedNameAttribute[])t.GetCustomAttributes(typeof(LocalizedNameAttribute), false);
            if (attributes != null &&
                attributes.Length > 0)
            {
                return attributes[0].DisplayName;
            }
            else
            {
                return String.Empty;
            }
        }
        /// <summary>
        /// Returns default value of the enum by [DefaultValue()] attribute
        /// </summary>
        public static T GetDefaultValue<T>() where T : struct
        {
            Type t = typeof(T);
            DefaultValueAttribute[] attributes = (DefaultValueAttribute[])t.GetCustomAttributes(typeof(DefaultValueAttribute), false);
            if (attributes != null &&
                attributes.Length > 0)
            {
                return (T)attributes[0].Value;
            }
            else
            {
                return (T)Enum.GetValues(typeof(T)).GetValue(0);
            }
        }
        /// <summary>
        /// Returns dictionary of key and valuefor enum
        /// </summary>
        public static IDictionary<int, string> ToDictionary<T>() where T : struct
        {
            var result = new Dictionary<int, string>();
            Type t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                result.Add(key, field);
            }
            return result;
        }
    }
}
