using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calculate.WPF.Annotations;

namespace Calculate.WPF.Model
{
    public class TextInputModel : INotifyPropertyChanged
    {
        private string textInput;

        public string TextInput
        {
            get { return textInput; }
            set
            {
                textInput = value;
                OnPropertyChanged("TextInput");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
