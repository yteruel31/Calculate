namespace Calculate.Lib.Operands
{
    public abstract class OperandBase
    {
        protected OperandBase(OperandType type)
        {
            Type = type;
        }

        protected OperandBase()
        {
        }

        public OperandType Type { get; set; }
    }
}