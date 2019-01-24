using System.Collections.ObjectModel;
using Calculate.Model;
using Calculate.WPF.Extensions;
using Calculate.WPF.Services;
using Calculate.WPF.ViewModel;
using MahApps.Metro.Controls.Dialogs;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelTests
{
    [TestFixture]
    public class When_LoadData_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataService;
        private Mock<IMainViewModelService> _mainViewModelService;
        private Mock<IDialogCoordinator> _dialog;
        private Mock<Formula> _formula;

        private MainViewModel GetSut()
        {
            _formulaDataService = new Mock<IFormulaDataService>();
            _mainViewModelService = new Mock<IMainViewModelService>();
            _dialog = new Mock<IDialogCoordinator>();
            _formula = new Mock<Formula>();
            _dialog.Setup(d => d.ShowMessageAsync(It.IsAny<Mock<Formula>>(), It.IsAny<string>(), It.IsAny<string>(), MessageDialogStyle.Affirmative, null));

            _formulaDataService.Setup(f => f.AddFormula(_formula.Object));
            _formulaDataService.Setup(f => f.GetAllFormulas().ToObservableCollection());
            return new MainViewModel(_formulaDataService.Object, _mainViewModelService.Object, _dialog.Object);
        }

        [Test]
        public void Check_If_Get_All_Data()
        {
            // Arrange
            MainViewModel sut = GetSut();
            // Act
            sut.LoadData();
            // Assert
            _formulaDataService.Verify(f => f.GetAllFormulas());
        }
    }
}