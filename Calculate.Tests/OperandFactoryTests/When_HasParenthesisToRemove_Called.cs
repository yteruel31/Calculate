using Calculate.Lib.Operands;
using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Test.OperandFactoryTests
{
    public class When_HasParenthesisToRemove_Called
    {
        [TestCase("(1+2)/6", ExpectedResult = false)]
        [TestCase("(2+3)", ExpectedResult = true)]
        [TestCase("(1+2)*(3+4)", ExpectedResult = false)]
        [TestCase("((1+2)*(3+4))", ExpectedResult = true)]
        [TestCase("(2+3))", ExpectedResult = false)]
        [TestCase("()", ExpectedResult = false)]
        [TestCase("((5)", ExpectedResult = false)]
        [TestCase("(5)", ExpectedResult = true)]
        [TestCase("((1+2)*3)", ExpectedResult = true)]
        public bool Default_case(string value)
        {
            return OperandFactory.HasParenthesisToRemove(value);
        }
    }
}