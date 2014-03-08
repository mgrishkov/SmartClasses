using System;
using System.Globalization;
using System.Linq;

namespace SmartClasses.Utils
{
    public class ResouceUtils
    {
        public static string GetStringResource(string fullItemName, CultureInfo culture)
        {
            var result = String.Empty;

            var split = fullItemName.Split('.').ToList();
            if (split.Count < 3)
            {
                throw new ArgumentException("Item name must have Assmbly Name, Namespace, Class and Item names");
            }
            var length = split.Count;
            var project = String.Join(".", split.GetRange(0, length - 3));
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(x => x.FullName.Contains(project));
            if (assembly == null)
            {
                throw new ArgumentException(String.Format("Assembly {0} does not found", project));
            }
            var resourceClass = String.Join(".", split.GetRange(0, length - 1));
            var resource = new System.Resources.ResourceManager(resourceClass, assembly);

            var itemName = split.Last();
            result = resource.GetString(itemName, culture);

            return result;
        }

        public static bool TryGetStringResource(string fullItemName, CultureInfo culture, out string itemValue)
        {
            var result = false;
            try
            {
                itemValue = GetStringResource(fullItemName, culture);
                result = true;
            }
            catch
            {
                itemValue = String.Empty;
            }
            ;
            return result;
        }
    }
}
