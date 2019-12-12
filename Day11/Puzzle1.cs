using System;
using AdventOfCode.Core;

namespace AdventOfCode.Day11
{
    public class Puzzle1 : AdventOfCode.Day11.Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            StartDrawing(isFirstPointWhite: false);
            return VisitedPoints.Count;
        }
    }
}