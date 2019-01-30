using System.Collections.Generic;
using System.Collections.ObjectModel;
using Calculate.Model;
using Calculate.WPF.Services;
using Calculate.WPF.ViewModel;
using FluentAssertions;
using MahApps.Metro.Controls.Dialogs;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_LoadData_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataService;
        private Mock<IMainViewModelService> _mainViewModelService;
        private Mock<IMainViewModel> _mainViewModel;
        private Mock<IDialogCoordinator> _dialog;

        private MainViewModelService GetSut()
        {
            _formulaDataService = new Mock<IFormulaDataService>();
            _mainViewModelService = new Mock<IMainViewModelService>();
            _mainViewModel = new Mock<IMainViewModel>();
            _dialog = new Mock<IDialogCoordinator>();
            _dialog.Setup(d => d.ShowMessageAsync(It.IsAny<Mock<Formula>>(), It.IsAny<string>(), It.IsAny<string>(),
                MessageDialogStyle.Affirmative, null));
            _mainViewModel.Setup(m => m.Formulas).Returns(GetObservableCollection());
            _formulaDataService.Setup(f => f.GetAllFormulas()).Returns(GetFormulas()).Callback(() => { });
            return new MainViewModelService(_formulaDataService.Object);
        }

        private static List<Formula> GetFormulas()
        {
            return new List<Formula> {new Formula {FormulaContent = "ffgh", Result = "aze"}};
        }

        private static ObservableCollection<Formula> GetObservableCollection()
        {
            return new ObservableCollection<Formula>(GetFormulas());
        }

        [Test]
        public void Check_If_Get_All_Data()
        {
            // Arrange
            MainViewModelService sut = GetSut();
            // Act
            var formulas = sut.LoadData();
            // Assert
            formulas.Count.Should().Be(1);
            formulas[0].FormulaContent.Should().Be("ffgh");
            formulas[0].Result.Should().Be("aze");
            _formulaDataService.Verify(f => f.GetAllFormulas(), Times.Once);
        }
    }
}