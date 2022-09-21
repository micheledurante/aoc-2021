
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day8
    {
        public static Dictionary<char, char> FindSegments(List<HashSet<char>> patterns)
        {
            var segments = new Dictionary<char, char>();

            var bd = new HashSet<char>(); // 4 - 1
            bd.UnionWith(patterns[2].Except(patterns[0]));

            var cde = new HashSet<char>(); // [c,d,e] = (8 - 0) + (8 - 6) + (8 - 9)
            cde.UnionWith(patterns[9].Except(patterns[6]));
            cde.UnionWith(patterns[9].Except(patterns[7]));
            cde.UnionWith(patterns[9].Except(patterns[8]));

            var bcef = new HashSet<char>(); // [b,c,e,f] = (8 - 2) + (8 - 3) + (8 - 5)
            bcef.UnionWith(patterns[9].Except(patterns[3]));
            bcef.UnionWith(patterns[9].Except(patterns[4]));
            bcef.UnionWith(patterns[9].Except(patterns[5]));

            var de = new HashSet<char>(); // 4 - [b,c,e,f]
            // equivalent to mathematical ∆
            // https://stackoverflow.com/a/5620306
            de.UnionWith(patterns[2]);
            de.SymmetricExceptWith(bcef);

            segments.Add('a', patterns[1].Except(patterns[0]).First()); // a = 7 - 1
            segments.Add('c', cde.Except(de).First()); // c = [c,d,e] - [d,e]
            segments.Add('d', bd.Intersect(de).First()); // d = [b,d] ⋂ [d,e]
            segments.Add('e', cde.Except(new HashSet<char> { segments['c'] }).Except(new HashSet<char> { segments['d'] }).First()); // e = [c,d,e] - c - d
            segments.Add('f', patterns[0].Except(new HashSet<char> { segments['c'] }).First()); // f = 1 - c
            segments.Add('b', bd.Except(new HashSet<char> { segments['d'] }).First()); // b = [b,d] - d
            segments.Add('g', patterns[3].Except(patterns[2]).Except(new HashSet<char> { segments['a'] }).Except(new HashSet<char> { segments['e'] }).First()); // g = 8 - 4 - a - e

            return segments;
        }

        public static string GetDigit(Dictionary<char, char> segments, HashSet<char> number)
        {
            if (number.Except(new HashSet<char> { segments['a'], segments['c'], segments['d'], segments['e'], segments['g'] }).Count() == 0)
                return "2";
            else if (number.Except(new HashSet<char> { segments['a'], segments['c'], segments['d'], segments['f'], segments['g'] }).Count() == 0)
                return "3";
            else if (number.Except(new HashSet<char> { segments['a'], segments['b'], segments['d'], segments['f'], segments['g'] }).Count() == 0)
                return "5";
            else if (number.Except(new HashSet<char> { segments['a'], segments['b'], segments['c'], segments['e'], segments['f'], segments['g'] }).Count() == 0)
                return "0";
            else if (number.Except(new HashSet<char> { segments['a'], segments['b'], segments['d'], segments['e'], segments['f'], segments['g'] }).Count() == 0)
                return "6";
            else if (number.Except(new HashSet<char> { segments['a'], segments['b'], segments['c'], segments['d'], segments['f'], segments['g'] }).Count() == 0)
                return "9";

            return null;
        }

        public static int DecodeReading(Dictionary<char, char> segments, List<HashSet<char>> reading)
        {
            var number = new string[4];

            for (var i = 0; i < 4; i++)
            {
                if (reading[i].Count() == 2)
                    number[i] = "1";
                else if (reading[i].Count() == 3)
                    number[i] = "7";
                else if (reading[i].Count() == 4)
                    number[i] = "4";
                else if (reading[i].Count() == 5)
                    number[i] = GetDigit(segments, reading[i]);
                else if (reading[i].Count() == 6)
                    number[i] = GetDigit(segments, reading[i]);
                else if (reading[i].Count() == 7)
                    number[i] = "8";
            }

            return int.Parse(string.Concat(number));
        }

        public static (int, int) Run(string[] input)
        {
            var readings = input
                .Select(x => x.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries))
                .Select(
                    y => (
                        y[0]
                            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(z => new HashSet<char>(z))
                            .OrderBy(q => q.Count())
                            .ToList(),
                        y[1]
                            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(z => new HashSet<char>(z))
                            .ToList()
                    )
                )
                .ToList();

            return (
                readings
                    .Select(v => v.Item2.Where(x => x.Count() == 2 || x.Count() == 3 || x.Count() == 4 || x.Count() == 7).ToList().Count())
                    .Sum(),
                readings.Select(x => DecodeReading(FindSegments(x.Item1), x.Item2)).Sum()
            );
        }
    }
}
