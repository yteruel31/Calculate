using Calculate.Lib.Services;
using Calculate.Model;
using Calculate.WPF.Extensions;
using System.Collections.ObjectModel;

namespace Calculate.WPF.Services
{
    public class MainViewModelService : IMainViewModelService
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
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
            if (string.IsNullOrEmpty(textInput))
            {
                return false;
            }

            return true;
        }

        public bool CanParenthesisToFormula(string obj, string textInput)
        {
            if (textInput == null && obj == ")")
            {
                return false;
            }

            return true;
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
                operand.Calculate(OperandFactory.Create(textInput)).ToString());
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

        public string OperationToFormula(string obj, string textInput)
        {
            if (textInput.EndsWith(OperationModel.Addition.Value) ||
                textInput.EndsWith(OperationModel.Substract.Value) ||
                textInput.EndsWith(OperationModel.Multiply.Value) ||
                textInput.EndsWith(OperationModel.Divide.Value))
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