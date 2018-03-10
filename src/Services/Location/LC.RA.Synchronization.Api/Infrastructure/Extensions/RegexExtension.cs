using System.Text.RegularExpressions;

namespace LC.RA.Location.Api.Infrastructure.Extensions
{
    public static class RegexExtension
    {
        public static MatchCollection GetMatches(string content, string pattern)
        {
            var redex = new Regex(pattern, RegexOptions.Compiled);
            var collection = redex.Matches(content);
            return collection;
        }

        public static void Replace(ref string content, string pattern)
        {
            var redex = new Regex(pattern, RegexOptions.Compiled);
            content = redex.Replace(content, string.Empty);
        }
    }
}