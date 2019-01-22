using Calculate.DAL;
using Calculate.WPF.Services;
using Calculate.WPF.ViewModel;
using MahApps.Metro.Controls.Dialogs;

namespace Calculate.WPF
{
    public class ViewModelLocator
    {
        private static readonly IFormulaDataService FormulaDataService = new FormulaDataService(new FormulaRepository()); 
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel(FormulaDataService, DialogCoordinator.Instance);
    }
}
