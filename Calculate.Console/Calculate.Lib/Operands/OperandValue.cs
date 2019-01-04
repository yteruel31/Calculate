namespace Calculate.Lib.Operands
{
    public class OperandValue : OperandBase
    {
        public decimal Value { get; set; }
        
        public override decimal Calculate()
        {
            return Value;
        }
    }
}