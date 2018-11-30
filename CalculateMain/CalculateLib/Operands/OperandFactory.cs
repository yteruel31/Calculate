using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace CalculateLib.Operands
{
    public static class OperandFactory
    {
        private static Regex parenthesisRegex = new Regex(@"\((?<content>[^)]*)\)", RegexOptions.Compiled);

        public static OperandBase Create(string input)
        {
            bool isParenthesis = input.Contains("(");
            if (isParenthesis)
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

            bool isAddition = input.Contains("+");
            if (isAddition)
            {
                string leftOperand = GetLeftOperandOfAdditionString(input);
                string rightOperand = GetRightOperandOfAdditionString(input);
                return new OperandAddition()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            bool isSubstract = input.Contains("-");
            if (isSubstract)
            {
                string leftOperand = GetLeftOperandOfSubstractString(input);
                string rightOperand = GetRightOperandOfSubstractString(input);
                return new OperandSubstract()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            bool isMultiply = input.Contains("*");
            if (isMultiply)
            {
                string leftOperand = GetLeftOperandOfMultiplyString(input);
                string rightOperand = GetRightOperandOfMultiplyString(input);
                return new OperandMultiply()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            bool isDivide = input.Contains("/");
            if (isDivide)
            {
                string leftOperand = GetLeftOperandOfDivideString(input);
                string rightOperand = GetRightOperandOfDivideString(input);
                return new OperandDivide()
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            return null;
        }

        public static string GetLeftOperandOfDivideString(string input)
        {
            string[] inputString = input.Split('/');
            return inputString[0];
        }

        public static string GetRightOperandOfDivideString(string input)
        {
            return input.Substring(GetLeftOperandOfDivideString(input).Length + 1);
        }

        public static string GetLeftOperandOfMultiplyString(string input)
        {
            string[] inputString = input.Split('*');
            return inputString[0];
        }

        public static string GetRightOperandOfMultiplyString(string input)
        {
            return input.Substring(GetLeftOperandOfMultiplyString(input).Length + 1);
        }

        public static string GetLeftOperandOfSubstractString(string input)
        {
            string[] inputString = input.Split('-');
            return inputString[0];
        }

        public static string GetRightOperandOfSubstractString(string input)
        {
            return input.Substring(GetLeftOperandOfSubstractString(input).Length + 1);
        }

        public static string GetLeftOperandOfAdditionString(string input)
        {
            string[] inputString = input.Split('+');
            return inputString[0];
        }

        public static string GetRightOperandOfAdditionString(string input)
        {
            return input.Substring(GetLeftOperandOfAdditionString(input).Length + 1);
        }

        public static string GetOperationWithoutParenthesisString(string input)
        {
            return input.Contains("((") || input.Contains("))") || input.Contains("()")
                ? null
                : parenthesisRegex.Match(input).Groups["content"].Value;
        }
    }
}