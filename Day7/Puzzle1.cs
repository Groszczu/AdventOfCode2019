using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day7
{
    public class Puzzle1 : AdventOfCode.Day5.Puzzle
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

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}