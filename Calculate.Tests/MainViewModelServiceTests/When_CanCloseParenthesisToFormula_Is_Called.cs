using Calculate.WPF.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_CanCloseParenthesisToFormula_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataServiceMock;
        private MainViewModelService GetSut()
        {
            _formulaDataServiceMock = new Mock<IFormulaDataService>();
            return new MainViewModelService(_formulaDataServiceMock.Object);
        }

        [TestCase("(4+")]
        [TestCase("(")]
        [TestCase(null)]
        public void Default_Case(string textInput)
        {
            //Arrange
            MainViewModelService sut = GetSut();
            //Act
            bool isInteract = sut.CanCloseParenthesisToFormula(textInput);

            //Assert
            isInteract.Should().BeFalse();
        }
    }
}