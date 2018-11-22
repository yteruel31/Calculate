using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CalculateLib
{
    public class Base
    {
        Base(string input)
        {
            if (input.Contains('('.ToString()) || input.Contains(')'.ToString()))
            {
                string inputStringWithParentheses = GetParenthesis(input);
                string[] solvedParenthesis = Solve(inputStringWithParentheses);
                //string stringConverted =
            }
        }

        public static bool ValidInput(string inputString)
        {
            if (string.IsNullOrWhiteSpace(inputString) ||
                !char.IsNumber(inputString[inputString.Length - 1]))
                return false;

            foreach (var number in inputString)
                if (!char.IsNumber(number))
                    if (
                        number != '*' &&
                        number != '/' &&
                        number != '+' &&
                        number != '-' &&
                        number != '.'
                    )
                        return false;
            return true;
        }

        public static string[] Solve(string inputString)
        {
            string[] parsedInput = ParseInputString(inputString);
            LogicTree[] logicTree = LogicTree.BuildLogicalTree(parsedInput);
            logicTree = LeftOperand.MakeLogical(logicTree);
            logicTree = RightOperand.MakeLogical(logicTree);
            string[] answer = LogicTree.SolveLogicTree(logicTree);
            return answer;
        }

        public static string GetParenthesis(string inputString)
        {
            int firstIndex = 0;
            int lenghIndex = 0;
            for (int i = 0; i < inputString.Length; i++)
            {
                if (inputString[i] == '(')
                {
                    firstIndex = i;
                }

                lenghIndex++;
                if (inputString[i] == ')')
                {
                    break;
                }
            }

            return inputString.Substring(firstIndex, lenghIndex);
        }
        static string ConvertStringArrayToStringJoin(string[] array)
        {
            string result = string.Join("", array);
            return result;
        }
        public static string[] ParseInputString(string inputString)
        {
            var sizeOfString = 0;

            for (var i = 0; i < inputString.Length; i++)
                if (i < inputString.Length)
                    if (inputString[i].Equals('*') || inputString[i].Equals('/') || inputString[i].Equals('+') ||
                        inputString[i].Equals('-'))
                        sizeOfString++;
            sizeOfString = sizeOfString * 2 + 1;
            var parsedInput = new string[sizeOfString];

            var stringBuilder = new StringBuilder();
            sizeOfString = 0;
            foreach (var charInput in inputString)
                if (charInput != '*' && charInput != '/' && charInput != '+' && charInput != '-')
                {
                    stringBuilder.Append(charInput);
                }
                else
                {
                    parsedInput[sizeOfString] = stringBuilder.ToString();
                    stringBuilder.Clear();
                    sizeOfString++;
                    parsedInput[sizeOfString] = charInput.ToString();
                    sizeOfString++;
                }

            parsedInput[sizeOfString] = stringBuilder.ToString();

            return parsedInput;
        }
    }
}