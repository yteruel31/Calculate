using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.CalculationServiceTests
{
    [TestFixture]
    public class When_Calculate_Is_Called
    {
        private CalculationService _calculationService;
        [Test]
        public void Default_Case()
        {
            _calculationService = new CalculationService();
            Assert.AreEqual(45, _calculationService.Calculate(OperandFactory.Create("42+3")));
        }
    }
}