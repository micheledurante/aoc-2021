
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day7
    {
        public static (int, int) Run(string[] input)
        {
            var points = input[0]
                .Split(',')
                .Select(int.Parse)
                .OrderBy(x => x)
                .GroupBy(y => y)
                .ToDictionary(z => z.Key, z => z.Count());

            var costs = new List<int>();
            var costs_exp = new List<int>();

            for (var i = 0; i <= points.Last().Key; i++)
            {
                costs.Add(points.Select(x => Math.Abs(x.Key - i) * x.Value).Sum());
                // triangular numbers, n(n + 1) / 2
                costs_exp.Add(points.Select(x => Math.Abs(x.Key - i) * (Math.Abs(x.Key - i) + 1) / 2 * x.Value).Sum());
            }

            return (costs.Min(), costs_exp.Min());
        }
    }
}
