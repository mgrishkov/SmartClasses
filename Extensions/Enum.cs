using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SmartClasses.Attributes;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Returns enum's item description by [Description(Text)] attribute
        /// </summary>
        /// <example>
        /// Language.Russian.GetDescription()
        /// </example>
        public static string GetDescription(this Enum val)
        {
            DescriptionAttribute[] attr = (DescriptionAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attr.Length > 0) ? attr[0].Description : string.Empty;
        }
        /// <summary>
        /// Returns a list of enum's items descriptions by [Description(Text)] attribute
        /// </summary>
        /// <example>
        /// Language.Russian.GetDescriptions()
        /// </example>
        public static List<string> GetDescriptions(this Enum sender)
        {
            var items = new List<string>();
            var enumType = sender.GetType();
            foreach (var val in Enum.GetNames(enumType))
            {

                var attrs = (DescriptionAttribute[])enumType.GetField(val).GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs.Length > 0)
                {
                    items.Add(attrs.First().Description);
                };
            }
            return items;
        }
        /// <summary>
        /// Returns enum's item localization text by [LocalizedName(ResourceKey, ResourceType)] attribute
        /// </summary>
        /// <example>
        /// Language.Russian.GetLocalizedText()
        /// </example>
        public static string GetLocalizedText(this Enum val)
        {
            LocalizedNameAttribute[] attr = (LocalizedNameAttribute[])val.GetType().GetField(val.ToString()).GetCustomAttributes(typeof(LocalizedNameAttribute), false);
            return (attr.Length > 0) ? attr[0].DisplayName : string.Empty;
        }
        /// <summary>
        /// Returns enum's item localization text by [LocalizedName(ResourceKey, ResourceType)] attribute
        /// </summary>
        /// <example>
        /// Language.Russian.GetLocalizedText()
        /// </example>
        public static T ToEnum<T>(this Enum val) where T : struct
        {
            Type t = Enum.GetUnderlyingType(typeof(T));
            var value = Convert.ChangeType(val, t);
            return (T)value;
        }
    }
}
