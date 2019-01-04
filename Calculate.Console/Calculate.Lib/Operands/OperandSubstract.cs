namespace Calculate.Lib.Operands
{
    public class OperandSubstract : OperandFunctionBase
    {
        public override decimal Calculate()
        {
            return LeftOperand.Calculate() - RightOperand.Calculate();
        }
    }
}