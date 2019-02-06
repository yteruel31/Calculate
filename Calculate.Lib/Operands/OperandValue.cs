namespace Calculate.Lib.Operands
{
    public class OperandValue : OperandBase
    {
        public OperandValue(decimal value) : base(OperandType.Value)
        {
            Value = value;
        }

        public decimal Value { get; set; }
    }
}