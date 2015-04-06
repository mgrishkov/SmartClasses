using System;
using System.ComponentModel;

namespace SmartClasses.Generators.CS
{
    public enum ElementType
    {
		[Description("Page")]
		Page = 1,
		[Description("MenuItem")]
		MenuItem = 2,
		[Description("Placeholder")]
		Placeholder = 3,
    }
    public enum ElementUrlType
    {
		[Description("SelfUrl")]
		SelfUrl = 1,
		[Description("NavigateToUrl")]
		NavigateToUrl = 2,
    }
    public enum LayoutElementType
    {
		[Description("Any")]
		Any = 0,
		[Description("Page")]
		Page = 1,
		[Description("MenuItem")]
		MenuItem = 2,
		[Description("Placeholder")]
		Placeholder = 3,
		[Description("Widget")]
		Widget = 4,
    }
    public enum WidgetType
    {
		[Description("Basic")]
		Basic = 1,
		[Description("GlobalResource")]
		GlobalResource = 2,
		[Description("LocalDictionary")]
		LocalDictionary = 3,
		[Description("News")]
		News = 4,
    }
     
}