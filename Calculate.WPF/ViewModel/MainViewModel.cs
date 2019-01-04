using System;
using Calculate.Lib.Operands;
using Calculate.WPF.Model;
using Calculate.WPF.Utility;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;

namespace Calculate.WPF.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            TextModel = new TextInputModel();

            EqualCommand = new CustomCommand(EqualFormula, CanInteract);
            MinimizedWindowsCommand = new CustomCommand(ReduceWindows, CanInteract);
            DeleteCommand = new CustomCommand(DeleteFormula, CanInteract);
            DeleteAllCommand = new CustomCommand(DeleteAllFormula, CanInteract);
            CloseWindowsCommand = new CustomCommand(CloseWindows, CanInteract);
            OperationToFormulaCommand = new CustomCommand(OperationToFormula, CanInteract);
            NumberToFormulaCommand = new CustomCommand(NumberToFormula, CanInteract);
            ParenthesisToFormulaCommand = new CustomCommand(ParenthesisToFormula, CanInteract);
        }

        public ICommand CloseWindowsCommand { get; }

        public ICommand DeleteAllCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand EqualCommand { get; }

        public ICommand MinimizedWindowsCommand { get; }

        public ICommand NumberToFormulaCommand { get; }

        public ICommand OperationToFormulaCommand { get; }

        public ICommand ParenthesisToFormulaCommand { get; }

        public TextInputModel TextModel { get; set; }

        private static void ReduceWindows(object obj)
        {
            if (Application.Current.MainWindow != null && Application.Current.MainWindow.WindowState == WindowState.Normal)
            {
                Application.Current.MainWindow.WindowState = WindowState.Minimized;
            }
            if (Application.Current.MainWindow != null && Application.Current.MainWindow.WindowState == WindowState.Minimized)
            {
                Application.Current.MainWindow.WindowState = WindowState.Normal;
            }
        }

        private bool CanInteract(object obj)
        {
            return true;
        }

        private void CloseWindows(object obj)
        {
            if (Application.Current.MainWindow != null) Application.Current.MainWindow.Close();
        }

        private void DeleteAllFormula(object obj)
        {
            TextModel.TextInput = "";
        }

        private void DeleteFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput.Remove(TextModel.TextInput.Length - 1);
        }

        private void EqualFormula(object obj)
        {
            OperandBase operand = OperandFactory.Create(TextModel.TextInput);
            try
            {
                TextModel.TextInput = operand.Calculate().ToString();
            }
            catch (NullReferenceException e)
            {
                MessageBox.Show(e.ToString());
                TextModel.TextInput = "";
            }
            catch (DivideByZeroException e)
            {
                MessageBox.Show("Impossible de Div par 0");
                TextModel.TextInput = "";
            }
        }

        private void NumberToFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput + obj;
        }

        private void OperationToFormula(object obj)
        {
            if (TextModel.TextInput.EndsWith("+") ||
                TextModel.TextInput.EndsWith("-") ||
                TextModel.TextInput.EndsWith("*") ||
                TextModel.TextInput.EndsWith("/"))
            {
                TextModel.TextInput = TextModel.TextInput.Remove(TextModel.TextInput.Length - 1);
            }

            TextModel.TextInput = TextModel.TextInput + obj;
        }

        private void ParenthesisToFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput + obj;
        }
    }
}