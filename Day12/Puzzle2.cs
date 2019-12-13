using System;
using System.Linq;

namespace AdventOfCode.Day12
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private long CalculateResult()
        {
            var xStepsCycle = 0;
            var yStepsCycle = 0;
            var zStepsCycle = 0;

            var steps = 0;
            while (xStepsCycle == 0 || yStepsCycle == 0 || zStepsCycle == 0)
            {
                steps++;
                SimulateGravity();
                Step();

                if (xStepsCycle == 0 && Moons.All(m => m.Velocity.X == 0))
                {
                    xStepsCycle = steps;
                }
                
                if (yStepsCycle == 0 && Moons.All(m => m.Velocity.Y == 0))
                {
                    yStepsCycle = steps;
                }

                if (zStepsCycle == 0 && Moons.All(m => m.Velocity.Z == 0))
                {
                    zStepsCycle = steps;
                }
            }

            return 2 * Lcm(xStepsCycle, Lcm(yStepsCycle, zStepsCycle));
        }
    }
}