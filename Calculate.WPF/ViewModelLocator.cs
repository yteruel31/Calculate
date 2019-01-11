using Calculate.DAL;
using Calculate.WPF.Services;
using Calculate.WPF.ViewModel;

namespace Calculate.WPF
{
    public class ViewModelLocator
    {
        private static IFormulaDataService FormulaDataService = new FormulaDataService(new FormulaRepository()); 
        public static MainViewModel MainViewModel { get; } = new MainViewModel(FormulaDataService);
    }
}
