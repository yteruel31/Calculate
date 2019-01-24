using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Calculate.WPF.Utility
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        private readonly string _nameOfCommand;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public CustomCommand(Action<object> execute, Predicate<object> canExecute, string nameOfCommand)
        {
            this.execute = execute;
            this.canExecute = canExecute;
            _nameOfCommand = nameOfCommand;
        }

        public bool CanExecute(object parameter)
        {
            bool b = canExecute == null ? true : canExecute(parameter);
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            Logger.Info($"Exécution de la commande {_nameOfCommand} avec le paramètre {parameter}");
            execute(parameter);
        }
    }
}