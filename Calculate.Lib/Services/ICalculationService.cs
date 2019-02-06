using Calculate.Lib.Operands;

namespace Calculate.Lib.Services
{
    public interface ICalculationService
    {
        decimal Calculate(OperandBase operand);
    }
}