using System.Text.RegularExpressions;
namespace TheCSharpIllegibleLedger
{
    public class Replacer
    {
        private Regex regex;
        private string replacement;

        public Replacer(Regex regex, string replacement)
        {
            this.regex = regex;
            this.replacement = replacement;
        }
        public Replacer(string regexText, string replacement)
        {
            this.regex = CreateRegex(regexText);
            this.replacement = replacement;
        }
        public string Replace(string line)
        {
            return regex.Replace(line, $"$1{replacement}$2");
        }
        private static Regex CreateRegex(string regexText)
        {
            return new Regex(regexText, RegexOptions.Compiled);
        }
    }
}