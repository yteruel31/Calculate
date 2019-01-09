using System.ComponentModel;
using System.Runtime.CompilerServices;
using Calculate.Model;
using Calculate.Model.Annotations;

namespace Calculate.Model
{
    public class TextInputModel : INotifyPropertyChanged
    {
        private string _textInput;

        public string TextInput
        {
            get { return _textInput; }
            set
            {
                _textInput = value;
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
