using System;
using System.Windows.Input;

namespace Calculate.WPF.Utility
{
    public class CustomCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;
        private readonly string _nameOfCommand;

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public CustomCommand(Action<object> execute, Predicate<object> canExecute, string nameOfCommand)
        {
            _execute = execute;
            _canExecute = canExecute;
            _nameOfCommand = nameOfCommand;
        }

        public bool CanExecute(object parameter)
        {
            bool b = _canExecute?.Invoke(parameter) ?? true;
            return b;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public void Execute(object parameter)
        {
            Logger.Info($"Exécution de la commande {_nameOfCommand} avec le paramètre {parameter}");
            _execute(parameter);
        }
    }

    public static class CommandFactory
    {
        public static CustomCommand Create(Action<object> execute, Predicate<object> canExecute, string commandName)
        {
            return new CustomCommand(execute, canExecute, commandName);
        }
    }
}