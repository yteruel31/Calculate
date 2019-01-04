namespace Calculate.Lib.Operands
{
    public class OperandDivide : OperandFunctionBase
    {
        public override decimal Calculate()
        {
            return LeftOperand.Calculate() / RightOperand.Calculate();
        }
    }
}