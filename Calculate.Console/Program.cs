using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Calculate.Lib;
using Calculate.Lib.Operands;

namespace Calculate.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string inputString = System.Console.ReadLine();

                OperandBase operand = OperandFactory.Create(inputString);


                System.Console.WriteLine(operand.Calculate());
            }
        }
    }
}
