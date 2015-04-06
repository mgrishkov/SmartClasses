using System.Data.Common;
using System.Linq;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static bool ColumnExists(this DbDataReader reader, string columnName)
        {
            return Enumerable.Range(0, reader.FieldCount).Any(x => reader.GetName(x) == columnName);
        }
    }
}
