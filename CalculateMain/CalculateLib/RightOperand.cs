using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateLib
{
    class RightOperand : IOperand
    {
        public LogicTree[] MakeLogical(LogicTree[] logicalTree)
        {
            for (int i = 1; i < logicalTree.Length; i++)
            {
                if (logicalTree[i].Operator == '+' || logicalTree[i].Operator == '-')
                {
                    if (i + 2 < logicalTree.Length)
                    {
                        if (logicalTree[i + 2].Operator == '*' || logicalTree[i + 2].Operator == '/')
                        {
                            int j;
                            for (j = i + 2; j < logicalTree.Length; j++)
                            {
                                if (logicalTree[j].Operator != '*' && logicalTree[j].Operator != '/')
                                {
                                    logicalTree[i].RightOperand = logicalTree[j - 2];
                                    break;
                                }
                                j++;
                            }

                            if (logicalTree[i].RightOperand == null)
                            {
                                logicalTree[i].RightOperand = logicalTree[j - 2];
                            }
                        } else {
                            logicalTree[i].RightOperand = logicalTree[i + 1];
                        }
                    } else {
                        logicalTree[i].RightOperand = logicalTree[i + 1];
                    }
                }
                i++;
            }
            return logicalTree;
        }
    }
}
