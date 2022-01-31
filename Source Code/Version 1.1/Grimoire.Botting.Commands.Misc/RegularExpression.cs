using System.Text.RegularExpressions;

namespace Grimoire.Botting.Commands.Misc
{
    public class RegularExpression
    {
        public bool IsVar(string value)
        {
            return Regex.IsMatch(value, @"\[([^\)]*)\]");
        }

        public string GetVar(string value)
        {
            return Regex.Replace(value, @"[\[\]']+", "");
        }
    }
}
