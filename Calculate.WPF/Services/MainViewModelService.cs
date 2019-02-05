using Calculate.Lib.Services;
using Calculate.Model;
using Calculate.WPF.Extensions;
using System.Collections.ObjectModel;
using System.Globalization;
using Calculate.WPF.Services.Validation;

namespace Calculate.WPF.Services
{
    public class MainViewModelService : IMainViewModelService
    {
        private readonly IFormulaDataService _formulaDataService;

        public MainViewModelService(IFormulaDataService formulaDataService)
        {
            _formulaDataService = formulaDataService;
        }

        public void AddFormula(Formula formula)
        {
            _formulaDataService.AddFormula(formula);
        }

        public bool CanInteractWithSpecific(string textInput)
        {
            return !string.IsNullOrEmpty(textInput);
        }

        public bool CanCloseParenthesisToFormula(string textInput)
        {
            if (textInput == null) return false;
            return !textInput.EndsWith("(") &&
                   !InputValidation.IsEndWithOperator(textInput);
        }

        public void CleanHistory()
        {
            _formulaDataService.DeleteFormula();
        }

        public string DeleteFormula(string textInput)
        {
            return textInput.Remove(textInput.Length - 1);
        }
        public Formula EqualFormula(string textInput)
        {
            var operand = new CalculationService();
            return GetFormulaObject(textInput,
                operand.Calculate(OperandFactory.Create(textInput)).ToString(CultureInfo.CurrentCulture));
        }

        public Formula GetFormulaObject(string textInput, string result)
        {
            return new Formula()
            {
                FormulaContent = textInput,
                Result = result
            };
        }
        public ObservableCollection<Formula> LoadData()
        {
            return _formulaDataService.GetAllFormulas().ToObservableCollection();
        }

        public string NumberToFormula(string obj, string textInput)
        {
            return textInput + obj;
        }

        public string OperatorToFormula(string obj, string textInput)
        {
            if (InputValidation.IsEndWithOperator(textInput))
            {
                textInput = textInput.Remove(textInput.Length - 1);
            }

            return textInput + obj;
        }

        public string ParenthesisToFormula(string obj, string textInput)
        {
            if (textInput != null && textInput.Equals("("))
            {
                return textInput.Insert(1, "0)");
            }
            
            return textInput + obj;
        }
    }
}