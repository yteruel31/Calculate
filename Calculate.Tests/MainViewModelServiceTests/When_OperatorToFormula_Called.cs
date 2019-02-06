using Calculate.WPF.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.MainViewModelServiceTests
{
    [TestFixture]
    public class When_OperatorToFormula_Called
    {
        private Mock<IFormulaDataService> _formulaDataServiceMock;

        private MainViewModelService GetSut()
        {
            _formulaDataServiceMock = new Mock<IFormulaDataService>();
            return new MainViewModelService(_formulaDataServiceMock.Object);
        }

        [TestCase("+", ExpectedResult = "1+1+")]
        [TestCase("-", ExpectedResult = "1+1-")]
        [TestCase("*", ExpectedResult = "1+1*")]
        [TestCase("/", ExpectedResult = "1+1/")]
        public string Change_Last_Operator(string value)
        {
            // Arrange
            MainViewModelService sut = GetSut();
            // Act
            return sut.OperatorToFormula(value, "1+1+");
        }
    }
}