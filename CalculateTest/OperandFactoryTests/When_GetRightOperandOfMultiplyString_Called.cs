using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class When_GetRightOperandOfMultiplyString_Called
    {
        [TestCase("1*2", ExpectedResult = "2")]
        [TestCase("1*2*3", ExpectedResult = "2*3")]
        public string Decimal_Value_Case(string value)
        {
            return OperandFactory.GetRightOperandOfMultiplyString(value);
        }
    }
}