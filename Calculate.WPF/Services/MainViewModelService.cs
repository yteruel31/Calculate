using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using Calculate.Model;
using System.Globalization;

namespace Calculate.WPF.Services
{
    public class MainViewModelService : IMainViewModelService
    {
        public string OperationToFormula(string obj, string textInput)
        private readonly IMainViewModel _mainViewModel;

        public MainViewModelService(IFormulaDataService formulaDataService, IMainViewModel mainViewModel)
        {
            if (textInput.EndsWith(OperationModel.Addition.Value) ||
            _mainViewModel = mainViewModel;
                textInput.EndsWith(OperationModel.Multiply.Value) ||
                textInput.EndsWith(OperationModel.Divide.Value))
            {
                textInput = textInput.Remove(textInput.Length - 1);
            }

            return textInput + obj;
        }

        public string NumberToFormula(string obj, string textInput)
        {
            return textInput + obj;
        }

        public string ParenthesisToFormula(string obj, string textInput)
        {
            if (textInput.Equals("()"))
            {
                return textInput.Insert(1, "0");
            }

            return textInput + obj;
        }

        public string EqualFormula(string textInput)
        {
            CalculationService operand = new CalculationService();
            return operand.Calculate(OperandFactory.Create(textInput)).ToString(CultureInfo.CurrentCulture);
        }

        public Formula GetFormula(string textInput)
        {
            return new Formula()
            {
                FormulaContent = textInput,
                Result = EqualFormula(textInput)
            };
        }

        public string DeleteFormula(string textInput)
        {
            return textInput.Remove(textInput.Length - 1);
        }

        public bool CanParenthesisToFormula(string obj, string textInput)
        {
            if (textInput == null && obj == ")")
            {
                return false;
            }

            return true;
        }
        public bool CanEqual(string textInput)
        {
            if (textInput == null)
            {
                return false;
            }
            return true;
        }
    }
}