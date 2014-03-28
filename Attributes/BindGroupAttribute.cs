using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using SmartClasses.Utils;

namespace SmartClasses.Attributes
{
    public class BindGroupAttribute : Attribute
    {
        public string Group { get; private set; }
        public BindGroupAttribute(string group)
        {
            Group = group;
        }
        
        public static IEnumerable<PropertyInfo> GetPropertiesOfGroup(Type owner, string groupName)
        {
            var properties = from p in owner.GetProperties()
                             let attr = p.GetCustomAttributes(typeof(BindGroupAttribute), true)
                             where attr.Length == 1
                                && attr.Cast<BindGroupAttribute>().Any(x => x.Group == groupName)
                             select p;

            return properties;
        }
    }
}
