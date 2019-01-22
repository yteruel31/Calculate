using Calculate.Lib.Operands;
using Calculate.Model;
using Calculate.WPF.Extensions;
using Calculate.WPF.Services;
using Calculate.WPF.Utility;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculate.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private DataGridCellInfo _cellInfo;
        private ObservableCollection<Formula> _formulas;
        private bool _isOpenHistoryFlyout;
        private IFormulaDataService formulaDataService;

        public MainViewModel(IFormulaDataService formulaDataService, IDialogCoordinator dialogCoordinator)
        {
            this.formulaDataService = formulaDataService;
            _dialogCoordinator = dialogCoordinator;

            GetDataInRowCommand = new CustomCommand(GetDataInRow, CanInteract);
            HistoryFlyoutCommand = new CustomCommand(HistoryFlyout, CanInteract);
            EqualCommand = new CustomCommand(EqualFormula, CanEqual);
            DeleteCommand = new CustomCommand(DeleteFormula, CanInteract);
            DeleteAllCommand = new CustomCommand(DeleteAllFormula, CanInteract);
            OperationToFormulaCommand = new CustomCommand(OperationToFormula, CanInteract);
            NumberToFormulaCommand = new CustomCommand(NumberToFormula, CanInteract);
            ParenthesisToFormulaCommand = new CustomCommand(ParenthesisToFormula, CanParenthesisToFormula);

            ListButtons();
            LoadData();
        }

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public DataGridCellInfo CellInfo
        {
            get { return _cellInfo; }
            set
            {
                _cellInfo = value;
                SelectedFormula = _cellInfo.Item as Formula;
                OnPropertyChanged("CellInfo");
            }
        }

        public ICommand GetDataInRowCommand { get; set; }

        public ICommand DeleteAllCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand EqualCommand { get; }

        public ObservableCollection<Formula> Formulas
        {
            get { return _formulas; }
            set
            {
                _formulas = value;
                OnPropertyChanged("Formulas");
            }
        }

        public bool IsOpenHistoryFlyout
        {
            get { return _isOpenHistoryFlyout; }
            set
            {
                _isOpenHistoryFlyout = value;
                OnPropertyChanged("IsOpenHistoryFlyout");
            }
        }

        public ICommand NumberToFormulaCommand { get; }

        public ObservableCollection<string> NumericButtons { get; set; }

        public ICommand HistoryFlyoutCommand { get; set; }

        public ObservableCollection<string> OperationButtons { get; set; }

        public ICommand OperationToFormulaCommand { get; }

        public ICommand ParenthesisToFormulaCommand { get; }

        public Formula SelectedFormula { get; set; }

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

        private bool CanEqual(object obj)
        {
            if (TextInput == null)
            {
                return false;
            }
            return true;
        }

        private bool CanInteract(object obj)
        {
            return true;
        }

        private bool CanParenthesisToFormula(object obj)
        {
            if (TextInput == null && obj.ToString() == ")")
            {
                return false;
            }

            return true;
        }

        private void GetDataInRow(object obj)
        {
            TextInput = SelectedFormula.FormulaContent;
        }

        private void DeleteAllFormula(object obj)
        {
            TextInput = null;
        }

        private void DeleteFormula(object obj)
        {
            TextInput = TextInput.Remove(TextInput.Length - 1);
        }

        private async void EqualFormula(object obj)
        {
            try
            {
                OperandBase operand = OperandFactory.Create(TextInput);
                string result = operand.Calculate().ToString();
                Formula formula = new Formula()
                {
                    FormulaContent = TextInput,
                    Result = result
                };
                logger.Info("L'opération utilisée est : {Formula} = {Result}", TextInput, result);
                _formulas.Add(formula);
                TextInput = result;
            }
            catch (NullReferenceException e)
            {
                logger.Debug(e);
                await _dialogCoordinator.ShowMessageAsync(this, "Erreur", e.ToString());
                TextInput = null;
            }
            catch (DivideByZeroException e)
            {
                logger.Error(e,"Impossible de Div par 0");
                await _dialogCoordinator.ShowMessageAsync(this, "Erreur", "Impossible de diviser par 0");
                TextInput = null;
            }
        }

        private void ListButtons()
        {
            NumericButtons = new ObservableCollection<string>()
            {
                "9", "8", "7", "6", "5", "4", "3", "2", "1"
            };
            OperationButtons = new ObservableCollection<string>()
            {
                OperationModel.Addition.Value,
                OperationModel.Substract.Value,
                OperationModel.Multiply.Value,
                OperationModel.Divide.Value
            };
        }

        private void LoadData()
        {
            Formulas = formulaDataService.GetAllFormulas().ToObservableCollection();
        }

        private void NumberToFormula(object obj)
        {
            TextInput = TextInput + obj;
        }

        private void HistoryFlyout(object obj)
        {
            if (IsOpenHistoryFlyout == false)
            {
                IsOpenHistoryFlyout = true;
            }
        }

        private void OperationToFormula(object obj)
        {
            if (TextInput.EndsWith(OperationModel.Addition.Value) ||
                TextInput.EndsWith(OperationModel.Substract.Value) ||
                TextInput.EndsWith(OperationModel.Multiply.Value) ||
                TextInput.EndsWith(OperationModel.Divide.Value))
            {
                TextInput = TextInput.Remove(TextInput.Length - 1);
            }

            TextInput = TextInput + obj;
        }

        private void ParenthesisToFormula(object obj)
        {
            TextInput = TextInput + obj;
            if (TextInput.Equals("()"))
            {
                TextInput = TextInput.Insert(1, "0");
            }
        }
    }
}