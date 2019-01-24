namespace Calculate.Lib.Operands
{
    public class OperandFunctionBase : OperandBase
    {
        public OperandFunctionBase(OperandType type) : base(type)
        {
        }

        public OperandFunctionBase LeftOperand { get; set; }
        public OperandFunctionBase RightOperand { get; set; }
    }
}