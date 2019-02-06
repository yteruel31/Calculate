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
            OperandBase operand = OperandFactory.Create("42+12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Addition_Three_Operand_Case()
        {
            OperandBase operand = OperandFactory.Create("42+12+3");

            AssertIsOperandValue(((OperandFunctionBase)operand).LeftOperand, 42m);
            AssertIsOperand(((OperandFunctionBase)operand).RightOperand, 12, 3);
        }

        [Test]
        public void Decimal_Value_Case()
        {
            AssertIsOperandValue(OperandFactory.Create("42"), 42m);
        }

        [Test]
        public void Divide_Case()
        {
            OperandBase operand = OperandFactory.Create("42/12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Multiply_Case()
        {
            OperandBase operand = OperandFactory.Create("42*12");

            AssertIsOperand(operand, 42, 12);
        }

        [Test]
        public void Parenthesis_Priority_Case()
        {
            OperandBase operand = OperandFactory.Create("(1+2)*42");

            AssertIsOperandValue(((OperandFunctionBase)operand).RightOperand, 42);
            AssertIsOperand(((OperandFunctionBase)operand).LeftOperand, 1, 2);
        }

        [Test]
        public void Parenthesis_Unuseful_Case()
        {
            OperandBase operand = OperandFactory.Create("(1+2)");

            AssertIsOperand(operand, 1, 2);
        }

        [Test]
        public void Substract_Case()
        {
            OperandBase operand = OperandFactory.Create("42-12");

            AssertIsOperand(operand, 42, 12);
        }

        private static void AssertIsOperand(OperandBase operand, decimal left, decimal right)
        {
            Assert.IsAssignableFrom<OperandFunctionBase>(operand);
            AssertIsOperandValue(((OperandFunctionBase)operand).LeftOperand, left);
            AssertIsOperandValue(((OperandFunctionBase)operand).RightOperand, right);
        }

        private static void AssertIsOperandValue(OperandBase operand, decimal value)
        {
            Assert.IsAssignableFrom<OperandValue>(operand);
            Assert.AreEqual(value, ((OperandValue)operand).Value);
        }
    }
}