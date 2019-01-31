using Calculate.Model;
using System.Collections.ObjectModel;

namespace Calculate.WPF.Services
{
    public interface IMainViewModelService
    {
        void AddFormula(Formula formula);

        bool CanInteractWithSpecific(string textInput);

        bool CanParenthesisToFormula(string obj, string textInput);

        void CleanHistory();

        string DeleteFormula(string textInput);

        Formula EqualFormula(string textInput);

        Formula GetFormulaObject(string textInput, string result);

        ObservableCollection<Formula> LoadData();

        string NumberToFormula(string obj, string textInput);

        string OperationToFormula(string obj, string textInput);

        string ParenthesisToFormula(string obj, string textInput);
    }
}