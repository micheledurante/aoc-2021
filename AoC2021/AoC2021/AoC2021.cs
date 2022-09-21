using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public class AoC2021
    {
        private static string[] ReadInput(string name)
        {
            return File.ReadAllLines(@".\Data\" + name);
        }

        static void Main(string[] args)
        {
            //Console.WriteLine("Day 1: " + Day1.Run(ReadInput("day01.txt")));
            //Console.WriteLine("Day 4: " + Day4.Run(ReadInput("day04.txt")));
            //Console.WriteLine("Day 5: " + Day5.Run(ReadInput("day05.txt")));
            //Console.WriteLine("Day 6: " + Day6.Run(ReadInput("day06.txt")));
            Console.WriteLine("Day 7: " + Day7.Run(ReadInput("day07.txt")));
            //Console.WriteLine("Day 8: " + Day8.Run(ReadInput("day08.txt")));
            Console.WriteLine("Press any key to quit...");
            Console.ReadKey();
        }
    }
}
