using Calculate.Lib.Services;
using NUnit.Framework;

namespace Calculate.Tests.ParenthesisServiceTests
{
    public class When_GetGroups_Called
    {
        [TestCase("((1+2)*(4+3))", ExpectedResult = new[] {"((1+2)*(4+3))", "(1+2)", "(4+3)"})]
        public string[] Default_Case(string input)
        {
            return ParenthesisService.GetGroups(input);
        }
    }
}