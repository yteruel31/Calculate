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
        private readonly IFormulaDataService _formulaDataService;
        private readonly IMainViewModelService _mainViewModelService;

        public MainViewModel(IFormulaDataService formulaDataService, IMainViewModelService mainViewModelService, IDialogCoordinator dialogCoordinator)
        {
            _formulaDataService = formulaDataService;
            _dialogCoordinator = dialogCoordinator;
            _mainViewModelService = mainViewModelService;

            GetDataInRowCommand = new CustomCommand(GetDataInRow, CanInteract, nameof(GetDataInRowCommand));
            HistoryFlyoutCommand = new CustomCommand(HistoryFlyout, CanInteract, nameof(HistoryFlyoutCommand));
            EqualCommand = new CustomCommand(EqualFormula, CanEqual, nameof(EqualCommand));
            DeleteCommand = new CustomCommand(DeleteFormula, CanInteract, nameof(DeleteCommand));
            DeleteAllCommand = new CustomCommand(DeleteAllFormula, CanInteract, nameof(DeleteAllCommand));
            OperationToFormulaCommand = new CustomCommand(OperationToFormula, CanInteract, nameof(OperationToFormulaCommand));
            NumberToFormulaCommand = new CustomCommand(NumberToFormula, CanInteract, nameof(NumberToFormulaCommand));
            ParenthesisToFormulaCommand = new CustomCommand(ParenthesisToFormula, CanParenthesisToFormula, nameof(ParenthesisToFormulaCommand));

            ListButtons();
            LoadData();
        }

        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public DataGridCellInfo CellInfo
        {
            get { return _cellInfo; }
            set
            {
                _cellInfo = value;
                SelectedFormula = _cellInfo.Item as Formula;
                OnPropertyChanged(nameof(CellInfo));
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
                OnPropertyChanged(nameof(Formulas));
            }
        }

        public bool IsOpenHistoryFlyout
        {
            get { return _isOpenHistoryFlyout; }
            set
            {
                _isOpenHistoryFlyout = value;
                OnPropertyChanged(nameof(IsOpenHistoryFlyout));
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
            TextInput = _mainViewModelService.DeleteFormula(TextInput);
        }

        private async void EqualFormula(object obj)
        {
            try
            {
                Logger.Info($"L'opération utilisée est : {TextInput}");
                _formulas.Add(_mainViewModelService.GetFormula(TextInput));
                TextInput = _mainViewModelService.EqualFormula(TextInput);
                Logger.Info($"Le résultat est : {_mainViewModelService.EqualFormula(TextInput)}");
            }
            catch (NullReferenceException e)
            {
                Logger.Error(e);
                await _dialogCoordinator.ShowMessageAsync(this, "Erreur", e.ToString());
                TextInput = null;
            }
            catch (DivideByZeroException e)
            {
                Logger.Error(e,"Impossible de Div par 0");
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

        public void LoadData()
        {
            Formulas = _formulaDataService.GetAllFormulas().ToObservableCollection();
        }

        private void NumberToFormula(object obj)
        {
            TextInput = _mainViewModelService.NumberToFormula(obj as string, TextInput);
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
            TextInput = _mainViewModelService.OperationToFormula(obj as string, TextInput);
        }

        private void ParenthesisToFormula(object obj)
        {
            TextInput = _mainViewModelService.ParenthesisToFormula(obj as string, TextInput);
        }
    }
}