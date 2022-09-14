using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021
{
    public class Day1
    {
        public static (int, int) Run(string[] input)
        {
            var n = input.Select(x => Int32.Parse(x.Trim())).ToArray();
            var part1 = 0;
            var part2 = 0;

            for (var i = 0; i < n.Length - 1; i++)
            {
                if (n[i + 1] > n[i])
                {
                    part1++;
                }
            }

            float third = n.Length / 3;

            for (var i = 0; i < (Math.Floor(third) * 3) - 1; i++)
            {
                if (n[i + 3] > n[i])
                {
                    part2++;
                }
            }

            return (part1, part2);
        }
    }
}
