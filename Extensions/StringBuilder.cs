using System;
using System.Text;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static void AppendLineFormated(this StringBuilder s, string format, params object[] args)
        {
            s.AppendLine(String.Format(format, args));
        }

        public static string GetMD5Hash(this StringBuilder s)
        {
            return s.ToString().ToMD5Hash();
        }
    }
}
