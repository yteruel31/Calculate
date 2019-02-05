using Calculate.WPF.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_CanInteractWithOperator_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataServiceMock;

        private MainViewModelService GetSut()
        {
            _formulaDataServiceMock = new Mock<IFormulaDataService>();
            return new MainViewModelService(_formulaDataServiceMock.Object);
        }

        [TestCase("(")]
        [TestCase("(4+4)+(")]
        public void When_Operator_Is_Input_After_Parenthesis_Should_Be_False(string textInput)
        {
            // Arrange
            MainViewModelService sut = GetSut();
            // Act
            bool isInteract = sut.CanInteractWithOperator(textInput);
            // Assert
            isInteract.Should().BeFalse();
        }
    }
}