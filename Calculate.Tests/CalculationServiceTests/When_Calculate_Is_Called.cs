using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.CalculationServiceTests
{
    [TestFixture]
    public class When_Calculate_Is_Called
    {
        [Test]
        public void Default_Case()
        {
            // Arrange
            CalculationService calculationService = new CalculationService();
            // Act

            // Assert
            Assert.AreEqual(45, calculationService.Calculate(OperandFactory.Create("42+3")));
        }
    }
}