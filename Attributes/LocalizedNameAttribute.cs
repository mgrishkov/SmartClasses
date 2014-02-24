using System;
using System.Reflection;

namespace SmartClasses.Attributes
{
    public class LocalizedNameAttribute : Attribute
    {
        private readonly Type _resourceType;
        private readonly string _resourceKey;

        public LocalizedNameAttribute(string resourceKey, Type resourceType)
        {
            _resourceType = resourceType;
            _resourceKey = resourceKey;
            DisplayName = (string)_resourceType
                .GetProperty(_resourceKey, BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public)
                .GetValue(null, null);
        }

        public string DisplayName { get; private set; }
    }
}
