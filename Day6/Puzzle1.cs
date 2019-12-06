
using System;

namespace AdventOfCode.Day6
{
    public class Puzzle1 : Puzzle
    {
        public override int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            return ObjectsTree.TotalOrbits;
        }
    }

}