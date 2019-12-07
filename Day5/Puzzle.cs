using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day5
{
    

    

    public abstract class Puzzle : IPuzzle
    {
        protected List<int> Program { get; private set; }
        protected IntcodeComputer IntcodeComputer { get; set; }

        public void LoadInput(string inputPath)
        {
            Program = File.ReadAllText(inputPath)
                .Split(',')
                .Select(int.Parse)
                .ToList();
        }
        public abstract int GetResult();
    }
}

