using System;
using System.Collections.Generic;
using System.Linq;
using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using Moq;
using NUnit.Framework;

namespace Calculate.Tests.CalculationServiceTests
{
    public class When_Calculate_Is_Called
    {

        private CalculationService GetSut()
        {
            return new CalculationService();
        }

        [Test]
        public void Default_Case()
        {
            CalculationService calculationService = new CalculationService();
            Assert.AreEqual(45, calculationService.Calculate(OperandFactory.Create("42+3")));
        }

        [TestCase(OperandType.Addition, ExpectedResult = 12)]
        [TestCase(OperandType.Divide, ExpectedResult = 3)]
        [TestCase(OperandType.Multiply, ExpectedResult = 27)]
        [TestCase(OperandType.Substract, ExpectedResult = 6)]
        public int When_Type_Called(OperandType type)
        {
            // Arrange
            OperandFunctionBase operandFunctionBase = new OperandFunctionBase(type)
            {
                LeftOperand = new OperandValue(9),
                RightOperand = new OperandValue(3)
            };
            CalculationService sut = GetSut();
            // Act
            return (int)sut.Calculate(operandFunctionBase);
        }
    }
}