
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day4
    {
        const int NO_OF_BOARDS = 100;

        public static (int, int) Run(string[] input)
        {
            double[] draws = input[0].Split(',').Select(x => double.Parse(x)).ToArray();
            var boards = new Matrix<double>[NO_OF_BOARDS];
            var scores = new Matrix<double>[NO_OF_BOARDS];
            var part1 = 0;
            var part2 = 0;
            var counter = 0;
            var have_won = new Dictionary<int, int>();
            var boards_data = input.Skip(2).Where(x => x.Length > 0).ToArray();

            for (var i = 0; i <= boards_data.Length - 5; i += 5)
            {
                double[][] rows = new double[5][];

                for (var x = 0; x < 5; x++)
                {
                    rows[x] = boards_data[i + x].Split(' ').Where(y => y.Trim().Length != 0).Select(z => double.Parse(z)).ToArray();
                }

                boards[counter] = Matrix<double>.Build.DenseOfRowArrays(rows);
                scores[counter] = Matrix<double>.Build.Dense(5, 5, 1); // init with 1s
                counter++;
            }

            // extract bingo numbers
            for (var i = 0; i < draws.Length; i++)
            {
                for (var x = 0; x < NO_OF_BOARDS; x++)
                {
                    if (boards[x].Exists(y => y == draws[i]))
                    {
                        var pos = boards[x].Find(z => z == draws[i]);
                        scores[x][pos.Item1, pos.Item2] = 0;

                        if (scores[x].RowSums().Where(r => r == 0).Count() >= 1 || scores[x].ColumnSums().Where(c => c == 0).Count() >= 1)
                        {
                            part1 = (int)draws[i] * (int)boards[x].PointwiseMultiply(scores[x]).RowSums().Sum();
                            goto end_part_1;
                        }
                    }
                }
            }

            end_part_1:

            for (var i = 0; i < draws.Length; i++)
            {
                for (var x = 0; x < NO_OF_BOARDS; x++)
                {
                    if (boards[x].Exists(y => y == draws[i]))
                    {
                        var pos = boards[x].Find(z => z == draws[i]);
                        scores[x][pos.Item1, pos.Item2] = 0;

                        if (scores[x].RowSums().Where(r => r == 0).Count() >= 1 || scores[x].ColumnSums().Where(c => c == 0).Count() >= 1)
                        {
                            if (!have_won.ContainsKey(x))
                            {
                                if (have_won.Count != NO_OF_BOARDS - 1)
                                {
                                    have_won[x] = 1;
                                }
                                else
                                {
                                    part2 = (int)draws[i] * (int)boards[x].PointwiseMultiply(scores[x]).RowSums().Sum();
                                    goto end_part_2;
                                }
                            }
                        }
                    }
                }
            }

            end_part_2:

            return (part1, part2);
        }
    }
}
