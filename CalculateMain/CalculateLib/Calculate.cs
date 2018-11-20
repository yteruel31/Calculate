using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    public class Calculate
    {
        private LeftOperand LeftOperand;
        private RightOperand RightOperand;
        private String InputString { get ; set; }

        public bool ValidInput()
        {
            return true;
        }
        private string[] ParseInputString()
        {
            int sizeOfString = 0;

            for (int i = 0; i < InputString.Length; i++)
            {
                if (i < InputString.Length)
                {
                    if (InputString[i].Equals('*') || InputString[i].Equals('/') || InputString[i].Equals('+') || InputString[i].Equals('-'))
                        sizeOfString++;
                }
            }

            string[] parsedInput = new string[sizeOfString];

            StringBuilder stringBuilder = new StringBuilder();
            sizeOfString = 0;
            foreach (var charInput in InputString)
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