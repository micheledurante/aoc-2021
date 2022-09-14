
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day6
    {
        public const int PART_1 = 80;
        public const int PART_2 = 256;

        public static Int64 Evolve(List<Int64> states, int days)
        {
            var values = new Dictionary<Int64, Int64>() {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 }
            };

            foreach (var state in states)
            {
                if (values.ContainsKey(state))
                    values[state]++;
                else
                    values[state] = 1;
            }

            for (var i = 0; i < days; i++)
            {
                // new day
                Int64 new_states = 0;
                var new_values = new Dictionary<Int64, Int64>() {
                    { 0, 0 },
                    { 1, 0 },
                    { 2, 0 },
                    { 3, 0 },
                    { 4, 0 },
                    { 5, 0 },
                    { 6, 0 },
                    { 7, 0 },
                    { 8, 0 }
                };

                for (var x = 0; x < 9; x++)
                {
                    if (x == 0 && values[x] > 0)
                    {
                        new_values[6] += values[0];
                        new_states = values[0];
                    }
                    else if (x != 0 && values[x] > 0)
                    {
                        new_values[x - 1] += values[x];
                    }
                }

                new_values[8] += new_states;
                values = new_values;
            }

            return values.Select(x => x.Value).Sum();
        }

        public static (Int64, Int64) Run(string[] lines)
        {
            var states = lines[0].Split(',').Select(x => Int64.Parse(x)).ToList();

            return (Evolve(states, PART_1), Evolve(states, PART_2));
        }
    }
}
