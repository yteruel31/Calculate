using System;

namespace CalculateLib.Operands
{
    public class OperandFunctionBase : OperandBase
    {
        public OperandBase LeftOperand { get; set; }

        public OperandBase RightOperand { get; set; }
        public override decimal Calculate()
        {
            throw new NotImplementedException();
        }
    }
}