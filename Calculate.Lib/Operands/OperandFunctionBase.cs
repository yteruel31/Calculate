namespace Calculate.Lib.Operands
{
    public class OperandFunctionBase : OperandBase
    {
        public OperandFunctionBase(OperandType type) : base(type)
        {
        }

        public OperandBase LeftOperand { get; set; }
        public OperandBase RightOperand { get; set; }
    }
}