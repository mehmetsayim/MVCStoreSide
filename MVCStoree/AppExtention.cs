using System.Text.RegularExpressions;

namespace MVCStoreeWeb
{
    public static class AppExtention
    {
        public static string ToSafeUrlString(this string text) => Regex.Replace(string.Concat(text.Where(p => char.IsLetterOrDigit(p) || char.IsWhiteSpace(p))), @"\s+", "-").ToLower();
    }
}
