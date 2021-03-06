﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SmartClasses.Attributes;

namespace SmartClasses.Utils
{
    public class EnumUtils
    {
        /// <summary>
        /// Returns dictionary of key and localized text for enum
        /// </summary>
        public static IDictionary<int, string> ToLocalizedDictionary<T>()
            where T: struct
        {
            var result = new Dictionary<int, string>();
            var t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                var attr = (LocalizedNameAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(LocalizedNameAttribute), false);
                var text = (attr.Length > 0) ? attr[0].DisplayName : string.Empty;
                result.Add(key, text);
            }
            return result;
        }
        /// <summary>
        /// Returns dictionary of key and description text for enum
        /// </summary>
        public static IDictionary<int, string> ToDescriptionDictionary<T>()
            where T: struct
        {
            var result = new Dictionary<int, string>();
            var t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                var attr = (DescriptionAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(DescriptionAttribute), false);
                var text = (attr.Length > 0) ? attr[0].Description : string.Empty;
                result.Add(key, text);
            }
            return result;
        }

        public static IDictionary<int, string> ToStringValueDictionary<T>()
            where T : struct
        {
            var result = new Dictionary<int, string>();
            var t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                var attr = (StringValueAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(StringValueAttribute), false);
                var text = (attr.Length > 0) ? attr[0].Value : string.Empty;
                result.Add(key, text);
            }
            return result;
        }

        /// <summary>
        /// Returns localized text for enum
        /// </summary>
        public static string GetLocalizedText<T>()
            where T: struct
        {
            var t = typeof(T);
            var attributes = (LocalizedNameAttribute[])t.GetCustomAttributes(typeof(LocalizedNameAttribute), false);
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
        public static T GetDefaultValue<T>()
            where T: struct
        {
            var t = typeof(T);
            var attributes = (DefaultValueAttribute[])t.GetCustomAttributes(typeof(DefaultValueAttribute), false);
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
        public static IDictionary<int, string> ToDictionary<T>()
            where T: struct
        {
            var result = new Dictionary<int, string>();
            var t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var key = (int)itm;
                var field = Enum.GetName(t, itm);
                result.Add(key, field);
            }
            return result;
        }

        public static T FindByStringValue<T>(string value)
            where T : struct
        {
            var t = typeof(T);

            foreach (var itm in Enum.GetValues(t))
            {
                var field = Enum.GetName(t, itm);
                var attr = (StringValueAttribute[])itm.GetType().GetField(field).GetCustomAttributes(typeof(StringValueAttribute), false);
                var text = (attr.Length > 0) ? attr[0].Value : string.Empty;

                if (text == value)
                    return (T) itm;
            }
            return default(T);
        }
    }
}
