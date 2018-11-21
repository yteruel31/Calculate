using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    public class Base
    {
        public bool ValidInput(string inputString)
        {
            if (String.IsNullOrWhiteSpace(inputString) ||
                !Char.IsNumber(inputString[inputString.Length - 1]))
            {
                return false;
            }

            for (int i = 0; i < inputString.Length; i++)
            {
                if (!Char.IsNumber(inputString[i]))
                {
                    if (inputString[i] != '*' &&
                        inputString[i] != '/' &&
                        inputString[i] != '+' &&
                        inputString[i] != '-' &&
                        inputString[i] != '.')
                    {
                        return false;
                    }
                }
            }
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
        public static string[] ParseInputString(string inputString)
        {
            int sizeOfString = 0;

            for (int i = 0; i < inputString.Length; i++)
            {
                if (i < inputString.Length)
                {
                    if (inputString[i].Equals('*') || inputString[i].Equals('/') || inputString[i].Equals('+') || inputString[i].Equals('-'))
                        sizeOfString++;
                }
            }
            sizeOfString = sizeOfString * 2 + 1;
            string[] parsedInput = new string[sizeOfString];

            StringBuilder stringBuilder = new StringBuilder();
            sizeOfString = 0;
            foreach (var charInput in inputString)
            {
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
            }
            parsedInput[sizeOfString] = stringBuilder.ToString();

            return parsedInput;
        }
        
    }
}