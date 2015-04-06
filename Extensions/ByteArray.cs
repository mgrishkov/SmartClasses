namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static string ПуеString(this byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}
