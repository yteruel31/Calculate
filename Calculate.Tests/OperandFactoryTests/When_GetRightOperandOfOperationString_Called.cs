using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Test.OperandFactoryTests
{
    public class When_GetRightOperandOfOperationString_Called
    {
        [TestCase("1+2", ExpectedResult = "2")]
        [TestCase("1+2+3", ExpectedResult = "2+3")]
        [TestCase("422+12+36666", ExpectedResult = "12+36666")]
        [TestCase("1+2*3", ExpectedResult = "2*3")]
        [TestCase("1*2+3", ExpectedResult = "3")]
        public string Addition_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfOperationString(value, '+');
        }

        [TestCase("1-2", ExpectedResult = "2")]
        [TestCase("1-2-3", ExpectedResult = "2-3")]
        [TestCase("1-2*3", ExpectedResult = "2*3")]
        [TestCase("1*2-3", ExpectedResult = "3")]
        public string Substract_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfOperationString(value, '-');
        }

        [TestCase("1*2", ExpectedResult = "2")]
        [TestCase("1*2*3", ExpectedResult = "2*3")]
        public string Multiply_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfOperationString(value, '*');
        }

        [TestCase("1/2", ExpectedResult = "2")]
        [TestCase("1/2/3", ExpectedResult = "2/3")]
        public string Divide_Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfOperationString(value, '/');
        }
    }
}