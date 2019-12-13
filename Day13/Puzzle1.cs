using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day13
{
    public class Puzzle1 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            while (!IntcodeComputer.FinishedProgram)
            {
                IntcodeComputer.RunProgram(breakOnOutput: true);
                var column = IntcodeComputer.OutputValue;

                IntcodeComputer.RunProgram(breakOnOutput: true);
                var row = IntcodeComputer.OutputValue;

                IntcodeComputer.RunProgram(breakOnOutput: true);
                var tileId = IntcodeComputer.OutputValue;

                Tiles[new Point(column, row)] = (TileType)tileId;
            }

            return Tiles.Count(p => p.Value == TileType.Block);
        }
    }
}