using Calculate.Model;

namespace Calculate.WPF.Services
{
    public interface IMainViewModelService
    {
        string OperationToFormula(string obj, string textInput);

        string NumberToFormula(string obj, string textInput);

        string ParenthesisToFormula(string obj, string textInput);

        string EqualFormula(string textInput);

        Formula GetFormula(string textInput);

        string DeleteFormula(string textInput);

        bool CanParenthesisToFormula(string obj, string textInput);

        bool CanEqual(string textInput);
    }
}