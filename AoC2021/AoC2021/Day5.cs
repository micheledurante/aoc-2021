
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day5
    {
        public static List<(int, int)> GetPoints(string[] lines)
        {
            var points = new List<(int, int)>();

            foreach (string line in lines)
            {
                var split = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var start = split[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
                var end = split[1].Split(',').Select(x => Int32.Parse(x)).ToArray();

                if (start[1] == end[1])
                {
                    for (var i = 0; i <= Math.Abs(start[0] - end[0]); i++)
                    {
                        if (start[0] < end[0])
                            points.Add((start[0] + i, start[1]));
                        else
                            points.Add((start[0] - i, start[1]));
                    }
                }
                else if (start[0] == end[0])
                {
                    for (var i = 0; i <= Math.Abs(start[1] - end[1]); i++)
                    {
                        if (start[1] < end[1]) 
                            points.Add((start[0], start[1] + i));
                        else 
                            points.Add((start[0], start[1] - i));
                    }
                }
            }

            return points;
        }

        public static List<(int, int)> GetPointsDiagonal(string[] lines)
        {
            var points = new List<(int, int)>();

            foreach (string line in lines)
            {
                var split = line.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);
                var start = split[0].Split(',').Select(x => Int32.Parse(x)).ToArray();
                var end = split[1].Split(',').Select(x => Int32.Parse(x)).ToArray();

                if (start[1] == end[1])
                {
                    for (var i = 0; i <= Math.Abs(start[0] - end[0]); i++)
                    {
                        if (start[0] < end[0])
                            points.Add((start[0] + i, start[1]));
                        else
                            points.Add((start[0] - i, start[1]));
                    }
                }
                else if (start[0] == end[0])
                {
                    for (var i = 0; i <= Math.Abs(start[1] - end[1]); i++)
                    {
                        if (start[1] < end[1])
                            points.Add((start[0], start[1] + i));
                        else
                            points.Add((start[0], start[1] - i));
                    }
                }
                else
                {
                    for (var i = 0; i <= Math.Abs(start[0] - end[0]); i++)
                    {
                        var x = 0;
                        var y = 0;

                        if (start[0] < end[0])
                            x = start[0] + i;
                        else
                            x = start[0] - i;

                        if (start[1] < end[1])
                            y = start[1] + i;
                        else
                            y = start[1] - i;

                        points.Add((x, y));
                    }
                }
            }

            return points;
        }

        public static (int, int) Run(string[] lines)
        {
            var matches = new Dictionary<(int, int), int>();
            var matches_diagonal = new Dictionary<(int, int), int>();
            var points = GetPoints(lines);
            var points_diagonal = GetPointsDiagonal(lines);

            foreach (var point in points)
                if (matches.ContainsKey(point))
                    matches[point]++;
                else 
                    matches[point] = 1;

            foreach (var point_diagonal in points_diagonal)
                if (matches_diagonal.ContainsKey(point_diagonal))
                    matches_diagonal[point_diagonal]++;
                else
                    matches_diagonal[point_diagonal] = 1;

            return (matches.Where(x => x.Value > 1).Count(), matches_diagonal.Where(x => x.Value > 1).Count());
        }
    }
}
