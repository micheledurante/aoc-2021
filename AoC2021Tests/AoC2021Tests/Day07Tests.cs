using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    [TestClass]
    public class Day07Tests : UnitTest
    {
        [TestMethod]
        public void Run_GivenInputs_ShoulCorrectlyCalculateNumbers()
        {
            var input = new string[] { "16,1,2,0,4,2,7,1,2,14" };

            var result = Day7.Run(input);

            Assert.AreEqual((37, 168), result);
        }
    }
}
