
using System;

namespace AdventOfCode.Day6
{
    public class Puzzle1 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            return ObjectsTree.TotalOrbits;
        }
    }

}