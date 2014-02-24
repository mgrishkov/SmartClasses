using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
