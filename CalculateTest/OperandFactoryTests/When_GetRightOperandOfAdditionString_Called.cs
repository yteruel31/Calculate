using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class When_GetRightOperandOfAdditionString_Called
    {

        [TestCase("1+2", ExpectedResult = "2")]
        [TestCase("1+2+3", ExpectedResult = "2+3")]
        [TestCase("422+12+36666", ExpectedResult = "12+36666")]
        [TestCase("1+2*3", ExpectedResult = "2*3")]
        [TestCase("1*2+3", ExpectedResult = "3")]
        public string Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfAdditionString(value);
        }
    }
}