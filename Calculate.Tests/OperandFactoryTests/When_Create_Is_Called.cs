using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Calculate.Tests.OperandFactoryTests
{
    public class When_Create_Is_Called
    {
        [Test]
        public void Addition_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("42+12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Addition_Three_Operand_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("42+12+3");

            AssertIsOperandValue(operand.LeftOperand, 42m);
            AssertIsOperand(operand.RightOperand, 12, 3);
        }

        [Test]
        public void Decimal_Value_Case()
        {
            AssertIsOperandValue(OperandFactory.Create("42"), 42m);
        }

        [Test]
        public void Divide_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("42/12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Multiply_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("42*12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Parenthesis_Priority_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("(1+2)*42");

            AssertIsOperandValue(operand.RightOperand, 42);
            AssertIsOperand(operand.LeftOperand, 1, 2);
        }

        [Test]
        public void Parenthesis_Unuseful_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("(1+2)");

            AssertIsOperand(operand, 1, 2);
        }

        [Test]
        public void Substract_Case()
        {
            OperandFunctionBase operand = OperandFactory.Create("42-12");

            AssertIsOperand(operand, 42, 12);
        }

        private static void AssertIsOperand(OperandFunctionBase operand, decimal left, decimal right)
        {
            AssertIsOperandValue(operand.LeftOperand, left);
            AssertIsOperandValue(operand.RightOperand, right);
        }

        private static void AssertIsOperandValue(OperandFunctionBase operand, decimal value)
        {
            Assert.IsAssignableFrom<OperandFunctionBase>(operand);
            Assert.AreEqual(value, operand.Value);
        }
    }
}