using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day7
{
    public class Puzzle1 : Puzzle
    {
        public override int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            int maxOutput = int.MinValue;
            foreach (var phaseSettings in GetPermutations(new int[] { 0, 1, 2, 3, 4 }, 5))
            {
                maxOutput = Math.Max(OutputSignal(phaseSettings.ToArray()), maxOutput);
            }

            return maxOutput;
        }

        private int OutputSignal(int[] phaseSettings)
        {
            var lastOutput = 0;
            foreach (var setting in phaseSettings)
            {
                var computer = new IntcodeComputer(new int[] { setting, lastOutput });
                computer.RunProgram(Program, true);
                lastOutput = computer.OutputValue;
            }

            return lastOutput;
        }
    }
}