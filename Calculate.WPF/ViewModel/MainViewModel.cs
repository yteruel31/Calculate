using Calculate.Model;
using Calculate.WPF.Services;
using Calculate.WPF.Utility;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace Calculate.WPF.ViewModel
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private readonly IMainViewModelService _mainViewModelService;
        private DataGridCellInfo _cellInfo;
        private ICommand _cleanHistoryCommand;
        private ICommand _deleteAllCommand;
        private ICommand _deleteFormulaCommand;
        private ICommand _equalFormulaCommand;
        private ObservableCollection<Formula> _formulas;
        private ICommand _getDataInRowCommand;
        private ICommand _historyFlyoutCommand;
        private bool _isOpenHistoryFlyout;
        private ICommand _loadDataCommand;
        private ICommand _numberToFormulaCommand;
        private ICommand _operatorToFormulaCommand;
        private ICommand _openParenthesisToFormulaCommand;
        private string _textInput;
        private ICommand _closeParenthesisToFormulaCommand;

        public MainViewModel(IMainViewModelService mainViewModelService, IDialogCoordinator dialogCoordinator)
        {
            DialogCoordinator = dialogCoordinator;
            _mainViewModelService = mainViewModelService;
            ListButtons();
        }

        public DataGridCellInfo CellInfo
        {
            get => _cellInfo;
            set
            {
                _cellInfo = value;
                SelectedFormula = _cellInfo.Item as Formula;
                OnPropertyChanged(nameof(CellInfo));
            }
        }

        public ICommand CleanHistoryCommand =>
            _cleanHistoryCommand ?? (_cleanHistoryCommand =
                CommandFactory.Create(CleanHistory, CanInteract, nameof(CleanHistoryCommand)));

        public ICommand DeleteAllCommand => _deleteAllCommand ?? (_deleteAllCommand =
                CommandFactory.Create(DeleteAllFormula, CanInteractWithSpecific, nameof(DeleteAllCommand)));

        public ICommand DeleteFormulaCommand =>
            _deleteFormulaCommand ?? (_deleteFormulaCommand =
                CommandFactory.Create(DeleteFormula, CanInteractWithSpecific, nameof(DeleteFormulaCommand)));

        public ICommand EqualFormulaCommand =>
            _equalFormulaCommand ?? (_equalFormulaCommand =
                CommandFactory.Create(EqualFormula, CanEqual, nameof(EqualFormulaCommand)));

        public ObservableCollection<Formula> Formulas
        {
            get => _formulas;
            set
            {
                _formulas = value;
                OnPropertyChanged(nameof(Formulas));
            }
        }

        public ICommand GetDataInRowCommand =>
            _getDataInRowCommand ?? (_getDataInRowCommand =
                CommandFactory.Create(GetDataInRow, CanInteract, nameof(GetDataInRowCommand)));

        public ICommand HistoryFlyoutCommand =>
            _historyFlyoutCommand ?? (_historyFlyoutCommand =
                CommandFactory.Create(HistoryFlyout, CanInteract, nameof(HistoryFlyoutCommand)));

        public bool IsOpenHistoryFlyout
        {
            get => _isOpenHistoryFlyout;
            set
            {
                _isOpenHistoryFlyout = value;
                OnPropertyChanged(nameof(IsOpenHistoryFlyout));
            }
        }

        public ICommand LoadDataCommand =>
            _loadDataCommand ?? (_loadDataCommand =
                CommandFactory.Create(LoadData, CanInteract, nameof(LoadDataCommand)));

        public ICommand NumberToFormulaCommand =>
            _numberToFormulaCommand ?? (_numberToFormulaCommand =
                CommandFactory.Create(NumberToFormula, CanInteract, nameof(NumberToFormulaCommand)));

        public ObservableCollection<string> NumericButtons { get; set; }

        public ObservableCollection<string> OperationButtons { get; set; }

        public ICommand OperatorToFormulaCommand =>
            _operatorToFormulaCommand ?? (_operatorToFormulaCommand =
                CommandFactory.Create(OperatorToFormula, CanInteractWithOperator,
                    nameof(OperatorToFormulaCommand)));

        public ICommand OpenParenthesisToFormulaCommand =>
            _openParenthesisToFormulaCommand ?? (_openParenthesisToFormulaCommand =
                CommandFactory.Create(ParenthesisToFormula, CanOpenParenthesisToFormula,
                    nameof(OpenParenthesisToFormulaCommand)));
        public ICommand CloseParenthesisToFormulaCommand =>
            _closeParenthesisToFormulaCommand ?? (_closeParenthesisToFormulaCommand =
                CommandFactory.Create(ParenthesisToFormula, CanCloseParenthesisToFormula,
                    nameof(CloseParenthesisToFormulaCommand)));

        public Formula SelectedFormula { get; set; }

        public string TextInput
        {
            get => _textInput;
            set
            {
                _textInput = value;
                OnPropertyChanged(nameof(TextInput));
            }
        }

        private bool CanInteract(object obj)
        {
            return true;
        }

        private bool CanEqual(object obj)
        {
            return _mainViewModelService.CanEqual(TextInput);
        }

        private bool CanInteractWithSpecific(object obj)
        {
            return _mainViewModelService.CanInteractWithSpecific(TextInput);
        }
        private bool CanInteractWithOperator(object obj)
        {
            return _mainViewModelService.CanInteractWithOperator(TextInput);
        }

        private bool CanParenthesisToFormula(object obj)
        private bool CanCloseParenthesisToFormula(object obj)
        {
            return _mainViewModelService.CanCloseParenthesisToFormula(TextInput);
        }

        private void CleanHistory(object obj)
        {
            _mainViewModelService.CleanHistory();
            Formulas.Clear();
        }

        private void DeleteAllFormula(object obj)
        {
            TextInput = null;
        }

        private void DeleteFormula(object obj)
        {
            TextInput = _mainViewModelService.DeleteFormula(TextInput);
        }

        private async void EqualFormula(object obj)
        {
            try
            {
                Logger.Info($"L'opération utilisée est : {TextInput}");
                var formula = _mainViewModelService.EqualFormula(TextInput);
                Formulas.Add(formula);
                _mainViewModelService.AddFormula(formula);
                TextInput = formula.Result;
                Logger.Info($"Le résultat est : {_mainViewModelService.EqualFormula(TextInput)}");
            }
            catch (NullReferenceException e)
            {
                Logger.Error(e);
                await DialogCoordinator.ShowMessageAsync(this, "Erreur", e.ToString());
                TextInput = null;
            }
            catch (DivideByZeroException e)
            {
                Logger.Error(e, "Impossible de Div par 0");
                await DialogCoordinator.ShowMessageAsync(this, "Erreur", "Impossible de diviser par 0");
                TextInput = null;
            }
        }

        private void GetDataInRow(object obj)
        {
            TextInput = SelectedFormula.FormulaContent;
            IsOpenHistoryFlyout = false;
        }

        private void HistoryFlyout(object obj)
        {
            if (!IsOpenHistoryFlyout)
            {
                IsOpenHistoryFlyout = true;
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

        private void LoadData(object obj)
        {
            Formulas = _mainViewModelService.LoadData();
        }

        private void NumberToFormula(object obj)
        {
            TextInput = _mainViewModelService.NumberToFormula(obj as string, TextInput);
        }

        private void OperatorToFormula(object obj)
        {
            TextInput = _mainViewModelService.OperatorToFormula(obj as string, TextInput);
        }

        private void ParenthesisToFormula(object obj)
        {
            TextInput = _mainViewModelService.ParenthesisToFormula(obj as string, TextInput);
        }
    }
}