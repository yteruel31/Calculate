using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    public class LeftOperand
    {
        public static LogicTree[] MakeLogical(LogicTree[] logicalTree)
        {
            for (int i = 0; i < logicalTree.Length; i++)
            {
                if (logicalTree[i].Operator == '*' || logicalTree[i].Operator == '/')
                {
                    if (i - 2 >= 0)
                    {
                        if (logicalTree[i - 2].Operator == '+' || logicalTree[i - 2].Operator == '-')
                            logicalTree[i].LeftOperand = logicalTree[i - 1];
                        else
                            logicalTree[i].LeftOperand = logicalTree[i - 2];
                    }
                    else logicalTree[i].LeftOperand = logicalTree[i - 1];
                    logicalTree[i].RightOperand = logicalTree[i + 1];
                    logicalTree[i].Priority = 1;
                }
                else if (logicalTree[i].Operator == '+' || logicalTree[i].Operator == '-')
                {
                    if (i - 2 >= 0) 
                    {
                        for (int j = i - 2; j >= 0; j--)
                        {
                            if (logicalTree[j].Operator == '+' || logicalTree[j].Operator == '-')
                            {
                                logicalTree[i].LeftOperand = logicalTree[j];
                                break;
                            }
                            j--;
                        }
                        if (logicalTree[i].LeftOperand == null)
                            logicalTree[i].LeftOperand = logicalTree[i - 2];
                    }
                    else logicalTree[i].LeftOperand = logicalTree[i - 1];
                    logicalTree[i].Priority = 2;
                }
            }
            return logicalTree;
        }
    }
}
