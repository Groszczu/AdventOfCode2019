using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day5
{
    public abstract class Puzzle : IPuzzle
    {
        protected List<long> Program { get; private set; }
        protected IntcodeComputer IntcodeComputer { get; set; }

        public void LoadInput(string inputPath)
        {
            Program = File.ReadAllText(inputPath)
                .Split(',')
                .Select(long.Parse)
                .ToList();
        }
        public abstract string GetResult();
    }
}

