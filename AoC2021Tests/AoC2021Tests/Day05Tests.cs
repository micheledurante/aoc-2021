using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    [TestClass]
    public class Day05Tests : UnitTest
    {
        [TestMethod]
        public void GetPointsTests_VerticalPositive_ShouldReturnPoints()
        {
            var input = new string[] { "0,9 -> 5,9" };
            var expected = new List<(int, int)>();
            expected.Add((0, 9));
            expected.Add((1, 9));
            expected.Add((2, 9));
            expected.Add((3, 9));
            expected.Add((4, 9));
            expected.Add((5, 9));

            var actual = Day5.GetPoints(input);

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void GetPointsTests_VerticalNegative_ShouldReturnPoints()
        {
            var input = new string[] { "7,9 -> 1,9" };
            var expected = new List<(int, int)>();
            expected.Add((7, 9));
            expected.Add((6, 9));
            expected.Add((5, 9));
            expected.Add((4, 9));
            expected.Add((3, 9));
            expected.Add((2, 9));
            expected.Add((1, 9));

            var actual = Day5.GetPoints(input);

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void GetPointsTests_HoriontalPositive_ShouldReturnPoints()
        {
            var input = new string[] { "7,0 -> 7,4" };
            var expected = new List<(int, int)>();
            expected.Add((7, 0));
            expected.Add((7, 1));
            expected.Add((7, 2));
            expected.Add((7, 3));
            expected.Add((7, 4));

            var actual = Day5.GetPoints(input);

            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        [TestMethod]
        public void GetPointsTests_HoriontalNegative_ShouldReturnPoints()
        {
            var input = new string[] { "7,5 -> 7,0" };
            var expected = new List<(int, int)>();
            expected.Add((7, 5));
            expected.Add((7, 4));
            expected.Add((7, 3));
            expected.Add((7, 2));
            expected.Add((7, 1));
            expected.Add((7, 0));

            var actual = Day5.GetPoints(input);

            Assert.IsTrue(actual.SequenceEqual(expected));
        }
    }
}
