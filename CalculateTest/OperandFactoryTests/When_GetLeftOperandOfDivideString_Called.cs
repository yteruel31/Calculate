using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class When_GetLeftOperandOfDivideString_Called
    {
        [TestCase("1/2", ExpectedResult = "1")]
        [TestCase("1/2/3", ExpectedResult = "1")]
        public string Decimal_Value_Case(string value)
        {
            return OperandFactory.GetLeftOperandOfDivideString(value);
        }
    }
}