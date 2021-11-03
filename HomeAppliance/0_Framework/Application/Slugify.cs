﻿using System.Globalization;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.RegularExpressions;

namespace _0_Framework.Application
{
    public static class Slugify
    {
        public static string GenerateSlug(this string phrase)
        {
            var s = phrase.RemoveDiacritics().ToLower();
            s = Regex.Replace(s, @"[^\u0600-\u06FF\uFB8A\u067E\u0686\u06AF\u200C\u200Fa-z0-9\s-]", "");
            s = Regex.Replace(s, @"\s+", " ");
            s = s.Substring(0, s.Length <= 100 ? s.Length : 45).Trim();
            s = Regex.Replace(s, @"\s", "-");
            s = Regex.Replace(s, @"", "-");
            return s.ToLower();
        }
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;
            var normilizedString = text.Normalize(NormalizationForm.FormKC);
            var stringBuilder = new StringBuilder();

            foreach (var c in normilizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);

            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}