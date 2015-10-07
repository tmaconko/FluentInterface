using System.Linq;

namespace FluentInterface.Examples
{
    // ReSharper disable LoopCanBeConvertedToQuery
    // ReSharper disable InvertIf
    
    public static class StringParserUsingLinq
    {
        private const string SpecialCharacters = "balticamadeus";

        private static bool IsSpecialChar(char chr)
        {
            return SpecialCharacters.Contains(chr);
        }

        public static string ParseString(string str)
        {
            return str.ToLower()
                .Normalize()
                .Trim()
                .Where(char.IsLetterOrDigit)
                .Where(IsSpecialChar)
                .Select(char.ToString)
                .Aggregate(string.Concat)
                .ToUpper();
        }

        public static string ParseStringNonFluent(string str)
        {
            string loweredString = str.ToLower();

            string normalizedString = loweredString.Normalize();

            string trimmedString = normalizedString.Trim();

            string aggregatedString = string.Empty;

            foreach (var chr in trimmedString)
            {
                if (char.IsLetterOrDigit(chr))
                {
                    if (IsSpecialChar(chr))
                    {
                        string stringChar = chr.ToString();
                        aggregatedString = string.Concat(aggregatedString, stringChar);
                    }                    
                }
            }

            string upperedString = aggregatedString.ToUpper();

            return upperedString;
        }
    }
}
