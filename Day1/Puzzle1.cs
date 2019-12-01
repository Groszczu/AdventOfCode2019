using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode.Day1
{
    public class Puzzle1 : IPuzzle
    {
        private IEnumerable<int> modulesMasses;
        public void LoadInput(string inputPath)
        {
            var logFile = File.ReadAllLines(inputPath);
            modulesMasses = new List<string>(logFile)
                                .Select(line => int.Parse(line));
        }

        public string GetResult() 
            => modulesMasses.Select(n => n / 3 - 2).Sum().ToString();
    }
}