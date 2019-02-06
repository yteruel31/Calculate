using Calculate.Model;
using System.Collections.ObjectModel;

namespace Calculate.WPF.Services
{
    public interface IMainViewModelService
    {
        void AddFormula(Formula formula);

        bool CanCloseParenthesisToFormula(string textInput);

        bool CanEqual(string textInput);

        bool CanInteractWithOperator(string textInput);

        bool CanInteractWithSpecific(string textInput);

        bool CanOpenParenthesisToFormula(string textInput);

        void CleanHistory();

        string DeleteFormula(string textInput);

        Formula EqualFormula(string textInput);

        ObservableCollection<Formula> LoadData();

        string NumberToFormula(string obj, string textInput);

        string OperatorToFormula(string obj, string textInput);

        string ParenthesisToFormula(string obj, string textInput);
    }
}