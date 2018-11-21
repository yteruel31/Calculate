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

        private static LogicTree[] BuildLogicalTree(String[] parseInput)
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
    }
}
