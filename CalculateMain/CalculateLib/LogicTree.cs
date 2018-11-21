using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    public class LogicTree
    {
        public char Operator { get; set; }
        public decimal Number { get; set; }
        public int Priority { get; set; }
        public LogicTree LeftOperand { get; set; }
        public LogicTree RightOperand { get; set; }

        public static LogicTree[] BuildLogicalTree(string[] parseInput)
        {
            LogicTree[] logicTree = new LogicTree[parseInput.Length];
            for (int i = 0; i < parseInput.Length; i++)
                logicTree[i] = new LogicTree();

            for (int j = 0; j < parseInput.Length; j++)
            {
                if (parseInput[j] == "*" || parseInput[j] == "/" || parseInput[j] == "+" ||
                    parseInput[j] == "-")
                {
                    logicTree[j].Operator = Char.Parse(parseInput[j]);
                }
                else
                {
                    logicTree[j].Number = Decimal.Parse(parseInput[j]);
                }
            }
            return logicTree;
        }
        private decimal GetValue()
        {
            if (Operator == 0)
                return Number;
            else
                switch (Operator)
                {
                    case '+':
                        return LeftOperand.GetValue() + RightOperand.GetValue();
                    case '-':
                        return LeftOperand.GetValue() - RightOperand.GetValue();
                    case '*':
                        return LeftOperand.GetValue() * RightOperand.GetValue();
                    case '/':
                        return LeftOperand.GetValue() / RightOperand.GetValue();
                    default: return 0;
                }
        }
        public static string[] SolveLogicTree(LogicTree[] logicTree)
        {
            int answersIndex = 1;
            for (int i = 0; i < logicTree.Length; i++)
            {
                if (logicTree[i].Operator != '\0')
                    answersIndex++;
            }
            string[] answers = new string[answersIndex];
            answers[answers.Length - 1] = "La réponse est : " + logicTree[logicTree.Length - 2].GetValue();
            return answers;
        }
    }
}
