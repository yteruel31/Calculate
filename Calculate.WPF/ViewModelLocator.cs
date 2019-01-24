using Calculate.DAL;
using Calculate.WPF.Services;
using Calculate.WPF.ViewModel;
using MahApps.Metro.Controls.Dialogs;

namespace Calculate.WPF
{
    public class ViewModelLocator
    {
        private static readonly IFormulaDataService FormulaDataService = new FormulaDataService(new FormulaRepository());
        private static readonly IMainViewModelService MainViewModelService = new MainViewModelService();
        public static MainViewModel MainViewModel { get; set; } = new MainViewModel(FormulaDataService, MainViewModelService, DialogCoordinator.Instance);
    }
}