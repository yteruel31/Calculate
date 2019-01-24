using Calculate.Lib.Operands;

namespace Calculate.Lib.Services
{
    public class CalculationService
    {
        public decimal Calculate(OperandFunctionBase operand)
        {
            switch (operand.Type)
            {
                case OperandBase.OperandType.Addition:
                    return Calculate(operand.LeftOperand) + Calculate(operand.RightOperand);

                case OperandBase.OperandType.Substract:
                    return Calculate(operand.LeftOperand) - Calculate(operand.RightOperand);

                case OperandBase.OperandType.Multiply:
                    return Calculate(operand.LeftOperand) * Calculate(operand.RightOperand);

                case OperandBase.OperandType.Divide:
                    return Calculate(operand.LeftOperand) / Calculate(operand.RightOperand);

                case OperandBase.OperandType.Value:
                    return operand.Value;

                default:
                    return 0;
            }
        }
    }
}