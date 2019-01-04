﻿namespace Calculate.Lib.Operands
{
    public class OperandMultiply : OperandFunctionBase
    {
        public override decimal Calculate()
        {
            return LeftOperand.Calculate() * RightOperand.Calculate();
        }
    }
}