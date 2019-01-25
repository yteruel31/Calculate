using Calculate.Lib.Operands;

namespace Calculate.Lib.Services
{
    public class CalculationService : ICalculationService
    {
        public decimal Calculate(OperandBase operand)
        {
            switch (operand.Type)
            {
                case OperandType.Addition:
                    return Calculate(((OperandFunctionBase)operand).LeftOperand) + Calculate(((OperandFunctionBase)operand).RightOperand);

                case OperandType.Substract:
                    return Calculate(((OperandFunctionBase)operand).LeftOperand) - Calculate(((OperandFunctionBase)operand).RightOperand);

                case OperandType.Multiply:
                    return Calculate(((OperandFunctionBase)operand).LeftOperand) * Calculate(((OperandFunctionBase)operand).RightOperand);

                case OperandType.Divide:
                    return Calculate(((OperandFunctionBase)operand).LeftOperand) / Calculate(((OperandFunctionBase)operand).RightOperand);

                case OperandType.Value:
                    return ((OperandValue)operand).Value;

                default:
                    return 0;
            }
        }
    }
}