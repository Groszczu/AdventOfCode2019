using System;

namespace AdventOfCode.Day12
{
    public class Puzzle1 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            SimulateSteps(1000);
            return TotalEnergy;
        }
    }
}