using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day13
{
    public abstract class Puzzle : IPuzzle
    {
        protected List<long> Program { get; private set; }
        protected IntcodeComputer IntcodeComputer { get; private set; }

        protected Dictionary<Point, TileType> Tiles { get; private set; }

        public void LoadInput(string inputPath)
        {
            Program = File.ReadAllText(inputPath)
                .Split(',')
                .Select(long.Parse)
                .ToList();

            IntcodeComputer = new IntcodeComputer(Program);

            Tiles = new Dictionary<Point, TileType>();
        }

        public abstract string GetResult();
    }
}