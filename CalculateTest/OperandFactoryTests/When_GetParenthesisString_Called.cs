using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class When_GetOperationWithoutParenthesisString_Called
    {
        [TestCase("(2+3)", ExpectedResult = "2+3")]
        [TestCase("(5)", ExpectedResult = "5")]
        [TestCase("((5))", ExpectedResult = "(5)")]
        [TestCase("((1+2)*3)", ExpectedResult = "(1+2)*3")]
        public string Decimal_Value_Case(string value)
        {
            return OperandFactory.GetOperationWithoutParenthesisString(value);
        }
    }
}