using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calculate.Model.Annotations;
using MahApps.Metro.Controls.Dialogs;

namespace Calculate.WPF.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected IDialogCoordinator _dialogCoordinator;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}