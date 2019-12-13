using System;

namespace AdventOfCode.Day12
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private ulong CalculateResult()
        {
            for (ulong step = 1UL; ; step++)
            {
                SimulateGravity();
                Step();
                if (AreAllInInitialState())
                {
                    return step;
                }
            }
        }
    }
}