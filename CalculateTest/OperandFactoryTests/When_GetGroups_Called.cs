using CalculateLib.Operands;
using NUnit.Framework;

namespace CalculateTest.OperandFactoryTests
{
    public class When_GetGroups_Called
    {
        [TestCase("((1+2)*(4+3))", ExpectedResult = new[] {"((1+2)*(4+3))", "(1+2)", "(4+3)"})]
        public string[] Default_Case(string input)
        {
            return OperandFactory.GetGroups(input);
        }
    }
}