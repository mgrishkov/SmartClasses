using System.Web;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        public static bool IsCompatibilityModeEnabled(this HttpBrowserCapabilitiesBase browser)
        {
            return (browser.Browser == "IE" && browser.MajorVersion < 9);
        }
    }
}
