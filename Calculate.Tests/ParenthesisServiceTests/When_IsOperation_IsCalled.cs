using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Tests.ParenthesisServiceTests
{
    public class When_IsOperation_IsCalled
    {
        [TestCase("1+2", ExpectedResult = true)]
        [TestCase("(1+2)+2", ExpectedResult = true)]
        [TestCase("(1+2)*2", ExpectedResult = false)]
        [TestCase("1+(2+2)", ExpectedResult = true)]
        [TestCase("1*(2+2)", ExpectedResult = false)]
        public bool Addition_Default_Case(string input)
        {
            return ParenthesisService.IsOperation(input, "+");
        }

        [TestCase("1-2", ExpectedResult = true)]
        [TestCase("(1+2)-2", ExpectedResult = true)]
        [TestCase("(1+2)+2", ExpectedResult = false)]
        [TestCase("1-(2+2)", ExpectedResult = true)]
        [TestCase("1*(2+2)", ExpectedResult = false)]
        public bool Substract_Default_Case(string input)
        {
            return ParenthesisService.IsOperation(input, "-");
        }

        [TestCase("1*2", ExpectedResult = true)]
        [TestCase("(1+2)*2", ExpectedResult = true)]
        [TestCase("(1+2)+2", ExpectedResult = false)]
        [TestCase("1*(2+2)", ExpectedResult = true)]
        [TestCase("1-(2+2)", ExpectedResult = false)]
        public bool Multiply_Default_Case(string input)
        {
            return ParenthesisService.IsOperation(input, "*");
        }

        [TestCase("2/3", ExpectedResult = true)]
        [TestCase("(1+2)/2", ExpectedResult = true)]
        [TestCase("(1+2)+2", ExpectedResult = false)]
        [TestCase("1/(2+2)", ExpectedResult = true)]
        [TestCase("1*(2+2)", ExpectedResult = false)]
        public bool Divide_Default_Case(string input)
        {
            return ParenthesisService.IsOperation(input, "/");
        }
    }
}