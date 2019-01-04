using Calculate.Lib.Operands;
using NUnit.Framework;

// ReSharper disable InconsistentNaming

namespace Calculate.Test.OperandFactoryTests
{
    public class When_Create_Is_Called
    {
        [Test]
        public void Decimal_Value_Case()
        {
            OperandBase operand = OperandFactory.Create("42");

            AssertIsOperandValue(operand, 42m);
        }

        [Test]
        public void Addition_Case()
        {
            OperandBase operand = OperandFactory.Create("42+12");

            Assert.IsAssignableFrom<OperandAddition>(operand);
            OperandAddition addition = (OperandAddition) operand;
            AssertIsOperandValue(addition.LeftOperand, 42m);
            AssertIsOperandValue(addition.RightOperand, 12m);
        }

        [Test]
        public void Substract_Case()
        {
            OperandBase operand = OperandFactory.Create("42-12");

            Assert.IsAssignableFrom<OperandSubstract>(operand);
            OperandSubstract substract = (OperandSubstract) operand;
            AssertIsOperandValue(substract.LeftOperand, 42m);
            AssertIsOperandValue(substract.RightOperand, 12m);
        }

        [Test]
        public void Multiply_Case()
        {
            OperandBase operand = OperandFactory.Create("42*12");

            AssertIsOperand<OperandMultiply>(operand, 42, 12);
        }

        [Test]
        public void Divide_Case()
        {
            OperandBase operand = OperandFactory.Create("42/12");

            AssertIsOperand<OperandDivide>(operand, 42, 12);
        }

        [Test]
        public void Addition_Three_Operand_Case()
        {
            OperandBase operand = OperandFactory.Create("42+12+3");

            Assert.IsAssignableFrom<OperandAddition>(operand);
            OperandAddition addition = (OperandAddition) operand;
            AssertIsOperandValue(addition.LeftOperand, 42m);
            AssertIsOperand<OperandAddition>(addition.RightOperand, 12, 3);
        }

        [Test]
        public void Parenthesis_Unuseful_Case()
        {
            OperandBase operand = OperandFactory.Create("(1+2)");
            AssertIsOperand<OperandAddition>(operand, 1, 2);
        }

        [Test]
        public void Parenthesis_Priority_Case()
        {
            OperandBase operand = OperandFactory.Create("(1+2)*42");

            Assert.IsAssignableFrom<OperandMultiply>(operand);
            OperandMultiply multiply = (OperandMultiply) operand;
            AssertIsOperandValue(multiply.RightOperand, 42);
            AssertIsOperand<OperandAddition>(multiply.LeftOperand, 1, 2);
        }

        private void AssertIsOperand<T>(OperandBase operand, decimal left, decimal right)
            where T : OperandFunctionBase
        {
            Assert.IsAssignableFrom<T>(operand);
            T addition = (T) operand;
            AssertIsOperandValue(addition.LeftOperand, left);
            AssertIsOperandValue(addition.RightOperand, right);
        }

        private void AssertIsOperandValue(OperandBase operand, decimal value)
        {
            Assert.IsAssignableFrom<OperandValue>(operand);
            Assert.AreEqual(value, ((OperandValue) operand).Value);
        }
    }
}