using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Tests.ParenthesisServiceTests
{
    public class When_GetOperationInsideParenthesisString_Called
    {
        [TestCase("(2+3)", ExpectedResult = "2+3")]
        [TestCase("(5)", ExpectedResult = "5")]
        [TestCase("((5))", ExpectedResult = "(5)")]
        [TestCase("((1+2)*3)", ExpectedResult = "(1+2)*3")]
        [TestCase("(1+2)*3", ExpectedResult = "1+2")]
        [TestCase("5*(1+2)", ExpectedResult = "1+2")]
        public string Decimal_Value_Case(string value)
        {
            return ParenthesisService.GetOperationInsideParenthesisString(value);
        }
    }
}