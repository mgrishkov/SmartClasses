using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        
        public static String ToHEX(this Color c)
        {
            return String.Format("#{0:X2}{1:X2}{2:X2}", c.R, c.G, c.B);
        }

        public static String ToRGB(this Color c)
        {
            return String.Format("RGB({0},{1},{2})", c.R, c.G, c.B);
        }
    }
}
