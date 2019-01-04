using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Calculate.Lib.Operands
{
    public static class OperandFactory
    {
        private static Regex GetGroupsRegex =
            new Regex(@"(?=(\((?>[^()]+|(?<o>)\(|(?<-o>)\))*(?(o)(?!)|)\)))", RegexOptions.Compiled);

        private static Regex OutsideParenthesisRegex =
            new Regex(@"(?<before>.+)?\((.+)?\)(?<after>.+)?", RegexOptions.Compiled);

        private static Regex ParenthesisRegex = new Regex(@"\((?<content>.+)\)", RegexOptions.Compiled);
        public static OperandBase Create(string input)
        {
            if (HasParenthesisToRemove(input))
            {
                return Create(GetOperationInsideParenthesisString(input));
            }

            bool isValue = decimal.TryParse(input, out decimal outValue);
            if (isValue)
            {
                return new OperandValue
                {
                    Value = outValue
                };
            }

            if (IsOperation(input, "+"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '+');
                string rightOperand = GetRightOperandOfOperationString(input, '+');
                return new OperandAddition()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, "-"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '-');
                string rightOperand = GetRightOperandOfOperationString(input, '-');
                return new OperandSubstract()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, "*"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '*');
                string rightOperand = GetRightOperandOfOperationString(input, '*');
                return new OperandMultiply()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, "/"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '/');
                string rightOperand = GetRightOperandOfOperationString(input, '/');
                return new OperandDivide()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            return null;
        }

        public static string[] GetGroups(string input)
        {
            var match = GetGroupsRegex.Matches(input);
            var result = match.Cast<Match>().Select(x => x.Groups[1].Value);
            return result.ToArray();
        }

        public static string GetLeftOperandOfOperationString(string input, char operation)
        {
            if (IsOperation(input, operation.ToString()))
            {
                if (input.StartsWith("("))
                {
                    string[] regGroup = GetGroups(input);
                    return regGroup[0];
                }

                string[] inputString = input.Split(operation);
                return inputString[0];
            }

            return null;
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

        public static string GetRightOperandOfOperationString(string input, char operation)
        {
            if (IsOperation(input, operation.ToString()))
            {
                return input.Substring(GetLeftOperandOfOperationString(input, operation).Length + 1);
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

            if (match.Success)
            {
                if (match.Groups["before"].Value.EndsWith(operation) ||
                    match.Groups["after"].Value.StartsWith(operation))
                {
                    return true;
                }
            }

            return false;
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