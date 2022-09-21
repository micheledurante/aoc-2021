using AoC2021;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AoC2021Tests
{
    [TestClass]
    public class Day08Tests : UnitTest
    {
        private Dictionary<char, char> segments;

        [TestInitialize]
        /// Validity of the dictionary is tested in FindSegments_GivenAnOrderedPattern_ShouldFindSegments()
        public void Initialize()
        {
            segments = new Dictionary<char, char>();
            segments.Add('a', 'd');
            segments.Add('b', 'e');
            segments.Add('c', 'a');
            segments.Add('d', 'f');
            segments.Add('e', 'g');
            segments.Add('f', 'b');
            segments.Add('g', 'c');
        }

        [TestMethod]
        public void FindSegments_GivenAnOrderedPattern_ShouldFindSegments()
        {
            var input = new List<HashSet<char>>();
            input.Add(new HashSet<char> { 'a', 'b' }); //acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab
            input.Add(new HashSet<char> { 'd', 'a', 'b' }); //acedgfb cdfbe gcdfa fbcad cefabd cdfgeb eafb cagedb
            input.Add(new HashSet<char> { 'e', 'a', 'f', 'b' }); //acedgfb cdfbe gcdfa fbcad cefabd cdfgeb cagedb
            input.Add(new HashSet<char> { 'c', 'd', 'f', 'b', 'e' }); //acedgfb gcdfa fbcad cefabd cdfgeb cagedb
            input.Add(new HashSet<char> { 'g', 'c', 'd', 'f', 'a' }); //acedgfb fbcad cefabd cdfgeb cagedb
            input.Add(new HashSet<char> { 'f', 'b', 'c', 'a', 'd' }); //acedgfb cefabd cdfgeb cagedb
            input.Add(new HashSet<char> { 'c', 'e', 'f', 'a', 'b', 'd' }); //acedgfb cdfgeb cagedb
            input.Add(new HashSet<char> { 'c', 'd', 'f', 'g', 'e', 'b' }); //acedgfb cagedb
            input.Add(new HashSet<char> { 'c', 'a', 'g', 'e', 'd', 'b' }); //acedgfb
            input.Add(new HashSet<char> { 'a', 'c', 'e', 'd', 'g', 'f', 'b' });

            var result = Day8.FindSegments(input);

            Assert.AreEqual(segments['a'], result['a']);
            Assert.AreEqual(segments['b'], result['b']);
            Assert.AreEqual(segments['c'], result['c']);
            Assert.AreEqual(segments['d'], result['d']);
            Assert.AreEqual(segments['e'], result['e']);
            Assert.AreEqual(segments['f'], result['f']);
            Assert.AreEqual(segments['g'], result['g']);
        }

        [TestMethod]
        public void GetDigit_5Letters_ShouldReturn2()
        {
            var input = new HashSet<char> { 'g', 'c', 'd', 'f', 'a' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("2", result);
        }

        [TestMethod]
        public void GetDigit_5Letters_ShouldReturn3()
        {
            var input = new HashSet<char> { 'f', 'b', 'c', 'a', 'd' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("3", result);
        }

        [TestMethod]
        public void GetDigit_5Letters_ShouldReturn5()
        {
            var input = new HashSet<char> { 'c', 'd', 'f', 'b', 'e' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("5", result);
        }

        [TestMethod]
        public void GetDigit_6Letters_ShouldReturn0()
        {
            var input = new HashSet<char> { 'c', 'a', 'g', 'e', 'd', 'b' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("0", result);
        }

        [TestMethod]
        public void GetDigit_6Letters_ShouldReturn6()
        {
            var input = new HashSet<char> { 'c', 'd', 'f', 'g', 'b', 'e' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("6", result);
        }

        [TestMethod]
        public void GetDigit_6Letters_ShouldReturn9()
        {
            var input = new HashSet<char> { 'c', 'e', 'f', 'a', 'b', 'd' };

            var result = Day8.GetDigit(segments, input);

            Assert.AreEqual("9", result);
        }

        [TestMethod]
        public void DecodeReading_GivenARreading1_ShoulReturnTheConcatNumber()
        {
            var num_1 = new HashSet<char> { 'c', 'd', 'f', 'e', 'b' };
            var num_2 = new HashSet<char> { 'f', 'c', 'a', 'd', 'b' };
            var num_3 = new HashSet<char> { 'c', 'd', 'f', 'e', 'b' };
            var num_4 = new HashSet<char> { 'c', 'd', 'b', 'a', 'f' };
            var input = new List<HashSet<char>> { num_1, num_2, num_3, num_4 };

            var result = Day8.DecodeReading(segments, input);

            Assert.AreEqual(5353, result);
        }

        [TestMethod]
        public void DecodeReading_GivenARreading2_ShoulReturnTheConcatNumber()
        {
            var num_1 = new HashSet<char> { 'a', 'c', 'e', 'd', 'g', 'f', 'b' };
            var num_2 = new HashSet<char> { 'd', 'a', 'b' };
            var num_3 = new HashSet<char> { 'e', 'a', 'f', 'b' };
            var num_4 = new HashSet<char> { 'a', 'b' };
            var input = new List<HashSet<char>> { num_1, num_2, num_3, num_4 };

            var result = Day8.DecodeReading(segments, input);

            Assert.AreEqual(8741, result);
        }

        [TestMethod]
        public void Run_SingleInput_ShoulCorrectlyCalculateNumbers()
        {
            var input = new string[] { "acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf" };

            var result = Day8.Run(input);

            Assert.AreEqual((0, 5353), result);
        }

        [TestMethod]
        public void Run_MultipleInputs_ShoulCorrectlyCalculateNumbers()
        {
            var input = new string[] {
                "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb | fdgacbe cefdb cefbgd gcbe",
                "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec | fcgedb cgb dgebacf gc",
                "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef | cg cg fdcagb cbg",
                "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega | efabcd cedba gadfec cb",
                "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga | gecf egdcabf bgf bfgea",
                "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf | gebdcfa ecba ca fadegcb",
                "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf | cefg dcbef fcge gbcadfe",
                "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd | ed bcgafe cdgba cbgef",
                "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg | gbdfcae bgc cg cgb",
                "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc | fgae cfgab fg bagce"
            };

            var result = Day8.Run(input);

            Assert.AreEqual((26, 61229), result);
        }
    }
}
