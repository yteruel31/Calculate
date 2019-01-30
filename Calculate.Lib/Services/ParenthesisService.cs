using System.Linq;
using System.Text.RegularExpressions;

namespace Calculate.Lib.Services
{
    public static class ParenthesisService
    {
        public static readonly Regex ParenthesisRegex = new Regex(@"\((?<content>.+)\)", RegexOptions.Compiled);

        private static readonly Regex GetGroupsRegex =
                    new Regex(@"(?=(\((?>[^()]+|(?<o>)\(|(?<-o>)\))*(?(o)(?!)|)\)))", RegexOptions.Compiled);

        private static readonly Regex OutsideParenthesisRegex =
            new Regex(@"(?<before>.+)?\((.+)?\)(?<after>.+)?", RegexOptions.Compiled);

        public static string[] GetGroups(string input)
        {
            var match = GetGroupsRegex.Matches(input);
            var result = match.Cast<Match>().Select(x => x.Groups[1].Value);
            return result.ToArray();
        }

        public static string GetOperationInsideParenthesisString(string input)
        {
            var match = ParenthesisRegex.Match(input);
            if (match.Success)
            {
                return match.Groups["content"].Value;
            }

            return null;
        }

        public static bool HasParenthesisToRemove(string input)
        {
            if (!IsStartAndEndWithParenthesis(input))
            {
                return false;
            }

            if (!HasValueInsideParenthesis(input))
            {
                return false;
            }

            string[] groups = GetGroups(input);

            return groups.Contains(input);
        }

        public static bool IsOperation(string input, string operation)
        {
            var match = OutsideParenthesisRegex.Match(input);

            if (!input.Contains("(") || !input.Contains(")"))
            {
                if (input.Contains(operation))
                {
                    return true;
                }
            }
            if (!match.Success) return false;
            return match.Groups["before"].Value.EndsWith(operation) ||
                   match.Groups["after"].Value.StartsWith(operation);
        }

        private static bool HasValueInsideParenthesis(string input)
        {
            if (!ParenthesisRegex.IsMatch(input))
            {
                return false;
            }

            return true;
        }

        private static bool IsStartAndEndWithParenthesis(string input)
        {
            if (!input.Contains("(") && !input.Contains(")"))
            {
                return false;
            }

            return true;
        }
    }
}