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
        private ObservableCollection<Formula> _formulas;
        private bool _isOpenHistoryFlyout;
        private string _textInput;

        public MainViewModel(IMainViewModelService mainViewModelService, IDialogCoordinator dialogCoordinator)
        {
            DialogCoordinator = dialogCoordinator;
            _mainViewModelService = mainViewModelService;

            GetDataInRowCommand = new CustomCommand(GetDataInRow, CanInteract, nameof(GetDataInRowCommand));
            HistoryFlyoutCommand = new CustomCommand(HistoryFlyout, CanInteract, nameof(HistoryFlyoutCommand));
            EqualCommand = new CustomCommand(EqualFormula, CanEqual, nameof(EqualCommand));
            DeleteCommand = new CustomCommand(DeleteFormula, CanInteract, nameof(DeleteCommand));
            DeleteAllCommand = new CustomCommand(DeleteAllFormula, CanInteract, nameof(DeleteAllCommand));
            OperationToFormulaCommand =
                new CustomCommand(OperationToFormula, CanInteract, nameof(OperationToFormulaCommand));
            NumberToFormulaCommand = new CustomCommand(NumberToFormula, CanInteract, nameof(NumberToFormulaCommand));
            ParenthesisToFormulaCommand = new CustomCommand(ParenthesisToFormula, CanParenthesisToFormula,
                nameof(ParenthesisToFormulaCommand));
            LoadDataCommand = new CustomCommand(LoadData, CanInteract, nameof(LoadDataCommand));
            CleanHistoryCommand = new CustomCommand(CleanHistory, CanInteract, nameof(LoadDataCommand));
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

        public ICommand CleanHistoryCommand { get; set; }

        public ICommand DeleteAllCommand { get; }

        public ICommand DeleteCommand { get; }

        public ICommand EqualCommand { get; }

        public ObservableCollection<Formula> Formulas
        {
            get => _formulas;
            set
            {
                _formulas = value;
                OnPropertyChanged(nameof(Formulas));
            }
        }

        public ICommand GetDataInRowCommand { get; set; }

        public ICommand HistoryFlyoutCommand { get; set; }

        public bool IsOpenHistoryFlyout
        {
            get => _isOpenHistoryFlyout;
            set
            {
                _isOpenHistoryFlyout = value;
                OnPropertyChanged(nameof(IsOpenHistoryFlyout));
            }
        }

        public ICommand LoadDataCommand { get; }

        public ICommand NumberToFormulaCommand { get; }

        public ObservableCollection<string> NumericButtons { get; set; }

        public ObservableCollection<string> OperationButtons { get; set; }

        public ICommand OperationToFormulaCommand { get; }

        public ICommand ParenthesisToFormulaCommand { get; }

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

        private bool CanEqual(object obj)
        {
            return _mainViewModelService.CanEqual(TextInput);
        }

        private bool CanInteract(object obj)
        {
            return true;
        }

        private bool CanParenthesisToFormula(object obj)
        {
            return _mainViewModelService.CanParenthesisToFormula(obj as string, TextInput);
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
        private void OperationToFormula(object obj)
        {
            TextInput = _mainViewModelService.OperationToFormula(obj as string, TextInput);
        }

        private void ParenthesisToFormula(object obj)
        {
            TextInput = _mainViewModelService.ParenthesisToFormula(obj as string, TextInput);
        }
    }
}