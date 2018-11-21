using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CalculateLib;

namespace CalculateMain
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputString = Console.ReadLine();
            string[] answers = Base.Solve(inputString);
            foreach (var value in answers)
            {
                Console.WriteLine(value);
            }
        }
    }
}
