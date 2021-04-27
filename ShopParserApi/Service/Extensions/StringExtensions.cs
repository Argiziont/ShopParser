using System;

namespace ShopParserApi.Service.Extensions
{
    public static class StringExtensions
    {
        public static string SubstringJson(this string value, string start, string end)
        {
            var startIndex = value.IndexOf(start, StringComparison.Ordinal) + start.Length;
            var endIndex = value.IndexOf(end, StringComparison.Ordinal) - startIndex;

            var result = value.Substring(startIndex, endIndex).Trim();
            return result.Remove(result.Length - 1);
        }
    }
}