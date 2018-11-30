namespace CalculateLib.Operands
{
    public class OperandAddition : OperandFunctionBase
    {
        public override decimal Calculate()
        {
            return LeftOperand.Calculate() + RightOperand.Calculate();
        }
    }
}