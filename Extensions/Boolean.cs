namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static string ToSAPBoolean(this bool s)
        {
            return (s) ? "X" : " ";
        }
    }
}
