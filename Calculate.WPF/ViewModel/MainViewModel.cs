using Calculate.Lib.Operands;
using Calculate.Model;
using Calculate.WPF.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Calculate.WPF.ViewModel
{
    public class MainViewModel
    {
        public MainViewModel()
        {
            TextModel = new TextInputModel();

            EqualCommand = new CustomCommand(EqualFormula, CanInteract);
            DeleteCommand = new CustomCommand(DeleteFormula, CanInteract);
            DeleteAllCommand = new CustomCommand(DeleteAllFormula, CanInteract);
            OperationToFormulaCommand = new CustomCommand(OperationToFormula, CanInteract);
            NumberToFormulaCommand = new CustomCommand(NumberToFormula, CanInteract);
            ParenthesisToFormulaCommand = new CustomCommand(ParenthesisToFormula, CanParenthesisToFormula);

            ListButtons();
        }

        public ObservableCollection<string> Buttons { get; set; }

        public ICommand DeleteAllCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand EqualCommand { get; }

        public ICommand NumberToFormulaCommand { get; }

        public ICommand OperationToFormulaCommand { get; }

        public ICommand ParenthesisToFormulaCommand { get; }

        public TextInputModel TextModel { get; set; }

        private bool CanInteract(object obj)
        {
            return true;
        }

        private bool CanParenthesisToFormula(object obj)
        {
            if (TextModel.TextInput == null && obj.ToString() == ")")
            {
                return false;
            }
            return true;
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
                //MessageBox.Show(e.ToString());
                TextModel.TextInput = "";
            }
            catch (DivideByZeroException e)
            {
                //MessageBox.Show("Impossible de Div par 0");
                TextModel.TextInput = "";
            }
        }

        private void ListButtons()
        {
            Buttons = new ObservableCollection<string>()
            {
                "9", "8", "7", "6", "5", "4", "3", "2", "1"
            };
        }

        private void NumberToFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput + obj;
        }

        private void OperationToFormula(object obj)
        {
            if (TextModel.TextInput.EndsWith(OperationModel.Addition.Value) ||
                TextModel.TextInput.EndsWith(OperationModel.Substract.Value) ||
                TextModel.TextInput.EndsWith(OperationModel.Multiply.Value) ||
                TextModel.TextInput.EndsWith(OperationModel.Divide.Value))
            {
                TextModel.TextInput = TextModel.TextInput.Remove(TextModel.TextInput.Length - 1);
            }

            TextModel.TextInput = TextModel.TextInput + obj;
        }

        private void ParenthesisToFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput + obj;
            if (TextModel.TextInput.Equals("()"))
            {
                TextModel.TextInput = TextModel.TextInput.Insert(1, "0");
            }
        }
    }
}