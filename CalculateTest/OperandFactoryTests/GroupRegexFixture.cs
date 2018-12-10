using System.Linq;
using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class GroupRegexFixture
    {
        [TestCase("((1+2)*(4+3))")]
        [TestCase("(1+2)")]
        [TestCase("(4+3)")]
        public void Default_Case(string output)
        {
            string input = "((1+2)*(4+3))";
            var matches = OperandFactory.GroupRegex.Matches(input);

            Assert.IsTrue(matches.Any(m => m.Groups["content"].Value == output));
        }
    }
}