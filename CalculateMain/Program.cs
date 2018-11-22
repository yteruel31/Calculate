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
            while (true)
            {
                string inputString = Console.ReadLine();
                if (!Base.ValidInput(inputString))
                {
                    Console.WriteLine("Mauvaise entrée");
                }
                else
                {
                    string[] answers = Base.Solve(inputString);
                    foreach (var value in answers)
                    {
                        Console.WriteLine(value);
                    }
                }
            }
        }
    }
}