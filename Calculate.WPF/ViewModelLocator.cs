using Calculate.WPF.ViewModel;

namespace Calculate.WPF
{
    public class ViewModelLocator
    {
        private static MainViewModel mainViewModel = new MainViewModel();

        public static MainViewModel MainViewModel => mainViewModel;
    }
}
