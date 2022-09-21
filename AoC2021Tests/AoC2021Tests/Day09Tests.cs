using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    [TestClass]
    public class Day09Tests
    {
        [TestMethod]
        public void Run_GivenInputs_ShoulCorrectlyCalculateNumbers()
        {
            var input = new string[] {
                "2199943210",
                "3987894921",
                "9856789892",
                "8767896789",
                "9899965678"
            };

            var result = Day9.Run(input);

            Assert.AreEqual((15, 0), result);
        }
    }
}
