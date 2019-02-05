using Calculate.WPF.Services;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_CanEqual_Is_Called
    {
        private Mock<IFormulaDataService> _formulaDataServiceMock;

        private MainViewModelService GetSut()
        {
            _formulaDataServiceMock = new Mock<IFormulaDataService>();
            return new MainViewModelService(_formulaDataServiceMock.Object);
        }

        [TestCase(null)]
        [TestCase("(")]
        public void When_Equal_Is_Called_With_False_Input_Should_Be_False(string textInput)
        {
            //Arrange
            MainViewModelService sut = GetSut();

            //Act
            bool isInteract = sut.CanEqual(textInput);

            //Assert
            isInteract.Should().BeFalse();
        }
    }
}