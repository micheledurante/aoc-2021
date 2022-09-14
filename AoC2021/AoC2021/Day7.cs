
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
                .Select(z => (Pos: z.Key, Count: z.Count()))
                .ToList();

            var costs = new List<int>();
            var costs_exp = new List<int>();

            foreach (var point in points)
                costs.Add(points.Select(x => Math.Abs(x.Pos - point.Pos) * x.Count).Sum());

            // triangular numbers, n(n + 1) / 2
            foreach (var point in points)
                costs_exp.Add(points.Select(x => Math.Abs(x.Pos - point.Pos) * (Math.Abs(x.Pos - point.Pos) + 1) / 2 * x.Count).Sum());

            return (costs.Min(), costs_exp.Min());
        }
    }
}
