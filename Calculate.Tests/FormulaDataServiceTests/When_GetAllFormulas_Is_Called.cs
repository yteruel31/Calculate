using Calculate.DAL;
using Calculate.WPF.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Test.FormulaDataServiceTests
{
    [TestFixture]
    public class When_GetAllFormulas_Is_Called
    {
        [Test]
        public void When_Repo_Called()
        {
            // Arrange
            var mockRepository = new Mock<IFormulaRepository>();
            mockRepository.Setup(x => x.GetFormulas());
            var formulaDataService = new FormulaDataService(mockRepository.Object);
            // Act
            formulaDataService.GetAllFormulas();
            // Assert
            mockRepository.VerifyAll();
        }
    }
}