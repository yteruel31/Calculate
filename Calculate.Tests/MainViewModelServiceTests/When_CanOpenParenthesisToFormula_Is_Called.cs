using Calculate.WPF.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_CanOpenParenthesisToFormula_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataServiceMock;
        private MainViewModelService GetSut()
        {
            _formulaDataServiceMock = new Mock<IFormulaDataService>();
            return new MainViewModelService(_formulaDataServiceMock.Object);
        }

        [TestCase("4")]
        [TestCase(")")]
        public void When_OpenParenthesis_Is_Input_After_TextInput_Should_Be_False(string textInput)
        {
            //Arrange
            MainViewModelService sut = GetSut();
            //Act
            bool isInteract = sut.CanOpenParenthesisToFormula(textInput);

            //Assert
            isInteract.Should().BeFalse();
        }
    }
}