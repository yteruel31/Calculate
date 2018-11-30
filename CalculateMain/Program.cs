using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CalculateLib;
using CalculateLib.Operands;

namespace CalculateMain
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string inputString = Console.ReadLine();

                OperandBase operand = OperandFactory.Create(inputString);

                Console.WriteLine(operand.Calculate());
            }
        }
    }
}