using System;
using System.Windows.Media;

namespace SmartClasses.Utils
{
    public partial class ColorUtils
    {
        public static Color GetContrastBlackOrWhiteColor(Color c, int keyPoint = 128)
        {
            if ((c.R * 299 + c.G * 587 + c.B * 114) / 1000 >= keyPoint)
                return Colors.Black;
            else
                return Colors.White;
        }
    }
}
