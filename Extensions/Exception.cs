using System;
using System.Text;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static String GetMessage(this Exception ex)
        {
            var msg = new StringBuilder();

            msg.Append(ex.Message);

            var inner = ex.InnerException;

            while (inner != null)
            {
                msg.AppendLine().Append(inner.Message);
                inner = inner.InnerException;
            }

            return msg.ToString();            
        }
    }
}
