
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace AoC2021
{
    public class Day9
    {
        public static Matrix<double> BuildMatrix(string[] input)
        {
            // pad with 9s so we don't overflow the matrix borders
            var nines_padding = Enumerable.Repeat((double)9, input[0].Length + 2).ToArray();
            var matrix_rows = new double[input.Length + 2][];
            matrix_rows[0] = nines_padding;

            for (var i = 1; i <= input.Length; i++)
            {
                matrix_rows[i] = input[i - 1].ToCharArray().Select(x => double.Parse(x.ToString())).ToArray();
            }

            return Matrix<double>.Build.DenseOfRowArrays(matrix_rows);
        }

        public static (int, int) Run(string[] input)
        {
            var heights = BuildMatrix(input);
            var visited = Matrix<double>.Build.Dense(input.Length, input[0].Length, 0); // 1 will indicate a visited point

            return (0, 0);
        }
    }
}
