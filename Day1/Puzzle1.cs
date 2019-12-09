using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode.Day1
{
    public class Puzzle1 : IPuzzle
    {
        private List<int> modulesMasses;
        public void LoadInput(string inputPath)
        {
            modulesMasses = File.ReadAllLines(inputPath)
                                .Select(line => int.Parse(line))
                                .ToList();
        }

        public string GetResult()
            => modulesMasses.Select(n => n / 3 - 2).Sum().ToString();
    }
}