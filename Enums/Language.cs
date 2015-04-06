
using System.ComponentModel;

namespace SmartClasses.Enums
{
    [DefaultValue(RU)]
    public enum Language
    {
        [Description("Русский")]
        RU = 1,
        [Description("English")]
        EN  = 2
    }
}
