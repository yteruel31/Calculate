using Calculate.Lib.Operands;

namespace Calculate.Lib.Services
{
    public static class OperandFactory
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public static OperandBase Create(string input)
        {
            Logger.Debug($"Create({input})");
            if (ParenthesisService.HasParenthesisToRemove(input))
            {
                return Create(ParenthesisService.GetOperationInsideParenthesisString(input));
            }

            bool isValue = decimal.TryParse(input, out decimal outValue);
            if (isValue)
            {
                return new OperandValue(outValue);
            }

            if (ParenthesisService.IsOperation(input, "+"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '+');
                string rightOperand = GetRightOperandOfOperationString(input, '+');
                return new OperandFunctionBase(OperandType.Addition)
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (ParenthesisService.IsOperation(input, "-"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '-');
                string rightOperand = GetRightOperandOfOperationString(input, '-');
                return new OperandFunctionBase(OperandType.Substract)
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (ParenthesisService.IsOperation(input, "*"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '*');
                string rightOperand = GetRightOperandOfOperationString(input, '*');
                return new OperandFunctionBase(OperandType.Multiply)
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            if (ParenthesisService.IsOperation(input, "/"))
            {
                string leftOperand = GetLeftOperandOfOperationString(input, '/');
                string rightOperand = GetRightOperandOfOperationString(input, '/');
                return new OperandFunctionBase(OperandType.Divide)
                {
                    LeftOperand = Create(leftOperand),
                    RightOperand = Create(rightOperand)
                };
            }

            return null;
        }

        public static string GetLeftOperandOfOperationString(string input, char operation)
        {
            if (ParenthesisService.IsOperation(input, operation.ToString()))
            {
                if (input.StartsWith("("))
                {
                    string[] regGroup = ParenthesisService.GetGroups(input);
                    return regGroup[0];
                }

                string[] inputString = input.Split(operation);
                return inputString[0];
            }

            return null;
        }

        public static string GetRightOperandOfOperationString(string input, char operation)
        {
            if (ParenthesisService.IsOperation(input, operation.ToString()))
            {
                return input.Substring(GetLeftOperandOfOperationString(input, operation).Length + 1);
            }

            return null;
        }
    }
}