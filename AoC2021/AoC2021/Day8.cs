
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day8
    {
        public static (int, int) Run(string[] input)
        {
            var readings = input
                .Select(x => x.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                .Select(
                    y => (
                        y[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(z => new HashSet<char>(z)).OrderBy(q => q.Count()).ToList(),
                        y[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(z => new HashSet<char>(z)).OrderBy(q => q.Count()).ToList()
                    )
                )
                .ToList();
            var nums = new List<int>();

            foreach (var reading in readings)
            {
                
            }


            return (
                readings
                    .Select(v => v.Item2.Where(x => x.Count() == 2 || x.Count() == 3 || x.Count() == 4 || x.Count() == 7).ToList().Count())
                    .Sum(),
                0
            );
        }
    }
}
