using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day8
{
    public abstract class Puzzle : IPuzzle
    {
        protected const int ImageWidth = 25;
        protected const int ImageHeight = 6;
        protected List<List<int>> Layers { get; private set; }
        public void LoadInput(string inputPath)
        {
            var input = File.ReadAllText(inputPath);
            var allPixels = input.Select(c => (int)char.GetNumericValue(c)).ToList();
            
            Layers = allPixels
                .Select((n, i) => (n, i))
                .GroupBy(p => p.i / (ImageWidth * ImageHeight))
                .Select(g => new List<int>(g.Select(p => p.n)))
                .ToList();
        }   
        
        public abstract int GetResult();
    }
}