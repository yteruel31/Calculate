using Calculate.DAL;
using Calculate.Model;
using Calculate.WPF.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Test.FormulaDataServiceTests
{
    [TestFixture]
    public class When_AddFormula_Is_Called
    {
        [Test]
        public void Default_Case()
        {
            // Arrange
            var mockFormulaRepository = new Mock<IFormulaRepository>();
            mockFormulaRepository.Setup(x => x.AddFormula(It.IsAny<Formula>()));
            var formulaDataService = new FormulaDataService(mockFormulaRepository.Object);
            // Act
            formulaDataService.AddFormula(new Formula());
            // Assert
            mockFormulaRepository.VerifyAll();
        }
    }
}