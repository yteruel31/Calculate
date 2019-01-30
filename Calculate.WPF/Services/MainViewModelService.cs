using Calculate.Lib.Services;
using Calculate.Model;
using Calculate.WPF.Extensions;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

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

        public bool CanEqual(string textInput)
        {
            if (textInput == null)
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

        public string DeleteFormula(string textInput)
        {
            return textInput.Remove(textInput.Length - 1);
        }

        public Formula EqualFormula(string textInput)
        {
            try
            {
                var operand = new CalculationService();
                return GetFormulaObject(textInput,
                    operand.Calculate(OperandFactory.Create(textInput)).ToString(CultureInfo.CurrentCulture));
            }
            catch (StackOverflowException e)
            {
                Logger.Error(e);
                return null;
            }
        }

        public Formula GetFormulaObject(string textInput, string result)
        {
            try
            {
                return new Formula()
                {
                    FormulaContent = textInput,
                    Result = result
                };
            }
            catch (StackOverflowException e)
            {
                Logger.Error(e);
                return null;
            }
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
            if (textInput.Equals("()"))
            {
                return textInput.Insert(1, "0");
            }

            return textInput + obj;
        }
    }
}