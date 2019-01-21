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
        private bool _isOpenHistory;
        private IFormulaDataService formulaDataService;

        private readonly IDialogCoordinator _dialogCoordinator;

        public MainViewModel(IFormulaDataService formulaDataService, IDialogCoordinator dialogCoordinator)
        {
            this.formulaDataService = formulaDataService;
            _dialogCoordinator = dialogCoordinator;

            TextModel = new TextInputModel();

            DataGridRowDcCommand = new CustomCommand(DataGridRowDC, CanInteract);
            OpenFlyoutCommand = new CustomCommand(OpenFlyout, CanInteract);
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

        public ICommand DataGridRowDcCommand { get; set; }

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

        public bool IsOpenHistory
        {
            get { return _isOpenHistory; }
            set
            {
                _isOpenHistory = value;
                OnPropertyChanged("IsOpenHistory");
            }
        }

        public ICommand NumberToFormulaCommand { get; }

        public ObservableCollection<string> NumericButtons { get; set; }

        public ICommand OpenFlyoutCommand { get; set; }

        public ObservableCollection<string> OperationButtons { get; set; }

        public ICommand OperationToFormulaCommand { get; }

        public ICommand ParenthesisToFormulaCommand { get; }

        public Formula SelectedFormula { get; set; }

        public TextInputModel TextModel { get; set; }

        private bool CanEqual(object obj)
        {
            if (TextModel.TextInput == null)
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
            if (TextModel.TextInput == null && obj.ToString() == ")")
            {
                return false;
            }

            return true;
        }

        private void DataGridRowDC(object obj)
        {
            TextModel.TextInput = SelectedFormula.FormulaContent;
        }

        private void DeleteAllFormula(object obj)
        {
            TextModel.TextInput = null;
        }

        private void DeleteFormula(object obj)
        {
            TextModel.TextInput = TextModel.TextInput.Remove(TextModel.TextInput.Length - 1);
        }

        private async void EqualFormula(object obj)
        {
            try
            {
                OperandBase operand = OperandFactory.Create(TextModel.TextInput);
                string result = operand.Calculate().ToString();
                Formula formula = new Formula()
                {
                    FormulaContent = TextModel.TextInput,
                    Result = result
                };
                logger.Info("L'opération utilisée est : {Formula} = {Result}", TextModel.TextInput, result);
                _formulas.Add(formula);
                TextModel.TextInput = result;
            }
            catch (NullReferenceException e)
            {
                logger.Debug(e);
                await _dialogCoordinator.ShowMessageAsync(this, "Erreur", e.ToString());
                TextModel.TextInput = null;
            }
            catch (DivideByZeroException e)
            {
                logger.Error(e,"Impossible de Div par 0");
                await _dialogCoordinator.ShowMessageAsync(this, "Erreur", "Impossible de diviser par 0");
                TextModel.TextInput = null;
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
            TextModel.TextInput = TextModel.TextInput + obj;
        }

        private void OpenFlyout(object obj)
        {
            if (IsOpenHistory == false)
            {
                IsOpenHistory = true;
            }
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