using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CalculateLib.Operands
{
    public static class OperandFactory
    {
        private static Regex parenthesisRegex = new Regex(@"\((?<content>.+)\)$", RegexOptions.Compiled);

        public static OperandBase Create(string input)
        {
            if (HasParenthesisToRemove(input))
            {
                Create(GetOperationWithoutParenthesisString(input));
            }

            bool isValue = decimal.TryParse(input, out decimal outValue);
            if (isValue)
            {
                return new OperandValue
                {
                    Value = outValue
                };
            }

            if (IsOperation(input, '+'))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '+');
                string rightOperand = GetRightOperandOfOperationString(input, '+');
                return new OperandAddition()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, '-'))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '-');
                string rightOperand = GetRightOperandOfOperationString(input, '-');
                return new OperandSubstract()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, '*'))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '*');
                string rightOperand = GetRightOperandOfOperationString(input, '*');
                return new OperandMultiply()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (IsOperation(input, '/'))
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

        public static string[] GetGroups(string input)
        {
            const string pattern = @"(?=(\((?>[^()]+|(?<o>)\(|(?<-o>)\))*(?(o)(?!)|)\)))";
            IEnumerable<string> result = Regex.Matches(input, pattern).Cast<Match>().Select(x => x.Groups[1].Value);
            return result.ToArray();
        }

        private static bool HasValueInsideParenthesis(string input)
        {
            if (!parenthesisRegex.IsMatch(input))
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

        public static string GetLeftOperandOfOperationString(string input, char operation)
        {
            if (input.StartsWith("("))
            {
                string[] regGroup = GetGroups(input);
                return regGroup[0];
            }

            string[] inputString = input.Split(operation);
            return inputString[0];
        }

        public static string GetRightOperandOfOperationString(string input, char operation)
        {
            return input.Substring(GetLeftOperandOfOperationString(input, operation).Length + 1);
        }

        public static string GetOperationInsideParenthesisString(string input)
        {
            var match = parenthesisRegex.Match(input);
            if (match.Success)
            {
                return match.Groups["content"].Value;
            }

            return null;
        }
    }
}