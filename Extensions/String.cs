using System;
using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartClasses.Extensions
{
    public static partial class ExtensionMethods
    {
        private static readonly Hashtable MatchingRu = new Hashtable(34);
        private static readonly Hashtable MatchingEn = new Hashtable(27);

        static ExtensionMethods()
        {
            MatchingRu.Add(' ', ' ');
            MatchingRu.Add('а', 'a');
            MatchingRu.Add('б', 'b');
            MatchingRu.Add('в', 'v');
            MatchingRu.Add('г', 'g');
            MatchingRu.Add('д', 'd');
            MatchingRu.Add('е', 'e');
            MatchingRu.Add('ё', "e");
            MatchingRu.Add('ж', "zh");
            MatchingRu.Add('з', 'z');
            MatchingRu.Add('и', 'i');
            MatchingRu.Add('й', 'i');
            MatchingRu.Add('к', 'k');
            MatchingRu.Add('л', 'l');
            MatchingRu.Add('м', 'm');
            MatchingRu.Add('н', 'n');
            MatchingRu.Add('о', 'o');
            MatchingRu.Add('п', 'p');
            MatchingRu.Add('р', 'r');
            MatchingRu.Add('с', 's');
            MatchingRu.Add('т', 't');
            MatchingRu.Add('у', 'u');
            MatchingRu.Add('ф', 'f');
            MatchingRu.Add('х', "kh");
            MatchingRu.Add('ц', "ts");
            MatchingRu.Add('ч', "ch");
            MatchingRu.Add('ш', "sh");
            MatchingRu.Add('щ', "shch");
            MatchingRu.Add('ь', '\'');
            MatchingRu.Add('ы', 'y');
            MatchingRu.Add('ъ', "\"");
            MatchingRu.Add('э', 'e');
            MatchingRu.Add('ю', "yu");
            MatchingRu.Add('я', "ya");

            MatchingEn.Add(' ', ' ');
            MatchingEn.Add('a', 'а');
            MatchingEn.Add('b', 'б');
            MatchingEn.Add('c', 'ц');
            MatchingEn.Add('d', 'д');
            MatchingEn.Add('e', 'е');
            MatchingEn.Add('f', 'ф');
            MatchingEn.Add('g', 'г');
            MatchingEn.Add('h', 'х');
            MatchingEn.Add('i', 'и');
            MatchingEn.Add('j', 'ж');
            MatchingEn.Add('k', 'к');
            MatchingEn.Add('l', 'л');
            MatchingEn.Add('m', 'м');
            MatchingEn.Add('n', 'н');
            MatchingEn.Add('o', 'о');
            MatchingEn.Add('p', 'п');
            MatchingEn.Add('q', 'к');
            MatchingEn.Add('r', 'р');
            MatchingEn.Add('s', 'с');
            MatchingEn.Add('t', 'т');
            MatchingEn.Add('u', 'ю');
            MatchingEn.Add('v', 'в');
            MatchingEn.Add('w', 'в');
            MatchingEn.Add('x', "кс");
            MatchingEn.Add('y', 'и');
            MatchingEn.Add('z', 'з');
        }

        public static String ToWordXmlString(this String data)
        {
            return data.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;");
        }

        public static String ToXmlString(this String data)
        {
            return data.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        /// <summary>
        /// Validates E-Mail.
        /// </summary>
        /// <param name="email">E-mail string.</param>
        /// <returns>True - validated successfuly, False - otherwise.</returns>
        public static Boolean IsValidEmail(this String email)
        {
            const string pattern = @"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+(?:[a-z]{2,6}|[0-9]{1,3})$";

            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Translits given phrase.
        /// </summary>
        /// <param name="text">Phrase to translit.</param>
        /// <param name="toRussian">Defines the translit direction: True - to Russian, False - to English</param>
        /// <returns>Transliterated phrase.</returns>
        public static string Transliterate(this String text, Boolean toRussian)
        {
            var matchings = toRussian ? MatchingEn : MatchingRu;

            var textLength = text.Trim().Length;

            var transliterated = new StringBuilder();

            for (var i = 0; i < textLength; ++i)
            {
                var ch = text[i].ToString().ToLower().ToCharArray()[0];

                if (matchings.ContainsKey(ch))
                {
                    if (Char.IsUpper(text[i]))
                    {
                        var value = matchings[ch].ToString().ToUpper();
                        transliterated.Append(value);
                    }
                    else
                    {
                        transliterated.Append(matchings[text[i]]);
                    }
                }
                else
                {
                    transliterated.Append(ch);
                }
            }
            return transliterated.ToString();
        }

        /// <summary>
        /// Return MD5 hash from the string
        /// </summary>
        public static string ToMD5Hash(this string value)
        {
            var sb = new StringBuilder();
            if (!String.IsNullOrWhiteSpace(value))
            {
                var md5 = MD5.Create();
                var passwordBytes = Encoding.ASCII.GetBytes(value);
                var hash = md5.ComputeHash(passwordBytes);
                for (var i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                ;
            }
            ;
            return sb.ToString();
        }
    }
}
