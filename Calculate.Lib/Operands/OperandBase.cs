namespace Calculate.Lib.Operands
{
    public abstract class OperandBase
    {
        public OperandBase(OperandType type)
        {
            Type = type;
        }

        public OperandBase()
        {
        }

        public enum OperandType
        {
            Addition,
            Substract,
            Multiply,
            Divide,
            Value
        }

        public OperandType Type { get; set; }
        public decimal Value { get; set; }
    }
}