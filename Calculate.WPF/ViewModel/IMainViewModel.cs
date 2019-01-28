using System.Collections.ObjectModel;
using Calculate.Model;

namespace Calculate.WPF.ViewModel
{
    public interface IMainViewModel
    {
        ObservableCollection<Formula> Formulas { get; set; }

    }
}