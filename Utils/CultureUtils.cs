using System;
using System.Collections.Generic;
using System.Globalization;
using SmartClasses.Enums;

namespace SmartClasses.Utils
{
    public static class CultureUtils
    {
        static readonly String _defaultCultureName;
        static readonly CultureInfo _defaultCulture;
        static readonly CultureInfo _englishCulture;

        // Supported cultures
        static readonly Dictionary<String, CultureInfo> Cultures;

        public static String DefaultCultureName
        {
            get { return _defaultCultureName; }
        }

        public static CultureInfo DefaultCulture
        {
            get { return _defaultCulture; }
        }

        public static CultureInfo EnglishCulture
        {
            get { return _englishCulture; }
        }

        static CultureUtils()
        {
            const string englishCultureName = "en-GB";

            _defaultCultureName = "ru-RU";

            Cultures = new Dictionary<String, CultureInfo>();
            Cultures.Add(_defaultCultureName, CultureInfo.CreateSpecificCulture(_defaultCultureName)); /* DEFAULT */
            Cultures.Add(englishCultureName, CultureInfo.CreateSpecificCulture(englishCultureName));

            _defaultCulture = GetValidCulture(_defaultCultureName);
            _englishCulture = GetValidCulture(englishCultureName);
        }

        /// <summary>
        /// Returns a valid culture name based on "name" parameter. If "name" is not valid, it returns the default culture
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-Gb)</param>
        public static string GetValidCultureName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
                return DefaultCultureName; // return Default culture

            if (Cultures.ContainsKey(name))
                return name;

            // Find a close match. For example, if you have "en-US" defined and the user requests "en-GB", 
            // the function will return closest match that is "en-US" because at least the language is the same (ie English)            
            foreach (var c in Cultures)
            {
                if (c.Key.StartsWith(name.Substring(0, 2)))
                    return c.Key;
            }

            return DefaultCultureName; // return Default culture as no match found
        }

        /// <summary>
        /// Returns a valid culture based on "name" parameter. If "name" is not valid, it returns the default culture
        /// </summary>
        /// <param name="name">Culture's name (e.g. en-Gb)</param>
        public static CultureInfo GetValidCulture(String name)
        {
            var culture = GetValidCultureName(name);

            if (Cultures.ContainsKey(culture))
                return Cultures[culture];

            return CultureInfo.CreateSpecificCulture(culture);
        }

        public static CultureInfo GetCulture(Language language)
        {
            switch (language)
            {
                case Language.RU:
                    return DefaultCulture;
                case Language.EN:
                    return EnglishCulture;
                default:
                    return DefaultCulture;
            }
        }
        public static Language ToLanguage(CultureInfo culture)
        {
            if (culture == DefaultCulture)
            {
                return Language.RU;
            }
            else if (culture == EnglishCulture)
            {
                return Language.EN;
            }
            else
            {
                return Language.RU;
            };
        }
    }
}