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
            string trimmedString = str.ToLower().Normalize().Trim();

            string aggregatedString = string.Empty;

            foreach (var chr in trimmedString)
            {
                if (!char.IsLetterOrDigit(chr))
                    continue;

                if (!IsSpecialChar(chr))
                    continue;
                    
                aggregatedString = string.Concat(aggregatedString, chr.ToString());                        
            }

            return aggregatedString.ToUpper();
        }
    }
}
