
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
                        y[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList(),
                        y[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList()
                    )
                )
                .ToList();

            return (
                readings
                    .Select(v => v.Item2.Where(x => x.Length == 2 || x.Length == 3 || x.Length == 4 || x.Length == 7).ToList().Count())
                    .Sum(),
                0
            );
        }
    }
}
