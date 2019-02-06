using Calculate.Lib.Services;

namespace Calculate.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            while (true)
            {
                string inputString = System.Console.ReadLine();

                CalculationService operand = new CalculationService();

                System.Console.WriteLine(operand.Calculate(OperandFactory.Create(inputString)));
            }
        }
    }
}