using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SmartClasses.Utils
{
    public class AttributeUtils
    {
        public static IEnumerable<PropertyInfo> GetProperties<T>(Type owner) where T: Attribute
        {
            var properties = from p in owner.GetProperties()
                             let attr = p.GetCustomAttributes(typeof(T), true)
                             where attr.Length == 1
                             select p;
            return properties;
        }
    }
}
