using Calculate.Model;

namespace Calculate.WPF.Services.Validation
{
    public static class InputValidation
    {
        public static bool IsEndWithOperator(string textInput)
        {
            return textInput.EndsWith(OperationModel.Addition.Value) ||
                   textInput.EndsWith(OperationModel.Substract.Value) ||
                   textInput.EndsWith(OperationModel.Multiply.Value) ||
                   textInput.EndsWith(OperationModel.Divide.Value);
        }
        public static bool IsEndWithNumber(string textInput)
        {
            return textInput.EndsWith("9") ||
                   textInput.EndsWith("8") ||
                   textInput.EndsWith("7") ||
                   textInput.EndsWith("6") ||
                   textInput.EndsWith("5") ||
                   textInput.EndsWith("4") ||
                   textInput.EndsWith("3") ||
                   textInput.EndsWith("2") ||
                   textInput.EndsWith("1") ||
                   textInput.EndsWith("0");
        }
    }
}