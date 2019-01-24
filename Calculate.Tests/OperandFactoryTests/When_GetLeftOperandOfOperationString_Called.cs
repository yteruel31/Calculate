using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Test.OperandFactoryTests
{
    public class When_GetLeftOperandOfOperationString_Called
    {
        [TestCase("1+2", ExpectedResult = "1")]
        [TestCase("(1+2)*2", ExpectedResult = null)]
        [TestCase("1+2+3", ExpectedResult = "1")]
        [TestCase("1+2*3", ExpectedResult = "1")]
        [TestCase("1*2+3", ExpectedResult = "1*2")]
        [TestCase("(1+2)+3", ExpectedResult = "(1+2)")]
        [TestCase("1+(2/3)", ExpectedResult = "1")]
        [TestCase("(1*2)+(3-1)", ExpectedResult = "(1*2)")]
        public string Addition_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetLeftOperandOfOperationString(value, '+');
        }

        [TestCase("1-2", ExpectedResult = "1")]
        [TestCase("1-2-3", ExpectedResult = "1")]
        [TestCase("1-2*3", ExpectedResult = "1")]
        [TestCase("1*2-3", ExpectedResult = "1*2")]
        public string Substract_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetLeftOperandOfOperationString(value, '-');
        }

        [TestCase("1*2", ExpectedResult = "1")]
        [TestCase("1*2*3", ExpectedResult = "1")]
        public string Multiply_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetLeftOperandOfOperationString(value, '*');
        }

        [TestCase("1/2", ExpectedResult = "1")]
        [TestCase("1/2/3", ExpectedResult = "1")]
        public string Divide_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetLeftOperandOfOperationString(value, '/');
        }
    }
}