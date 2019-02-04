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
    }
}