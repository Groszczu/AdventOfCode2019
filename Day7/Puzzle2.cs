using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day7
{
    public class Puzzle2 : AdventOfCode.Day5.Puzzle
    {
        public override int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            int maxOutput = int.MinValue;
            foreach (var phaseSettings in GetPermutations(new int[] { 5, 6, 7, 8, 9 }, 5))
            {
                maxOutput = Math.Max(OutputSignal(phaseSettings.ToArray()), maxOutput);
            }

            return maxOutput;
        }

        public int OutputSignal(int[] phaseSettings)
        {
            var computers = new List<IntcodeComputer>();
            foreach (var setting in phaseSettings)
            {
                var computer = new IntcodeComputer(new int[] { setting });
                computers.Add(computer);
            }

            computers[0].AddInputValue(0);

            int? lastOutput = null;
            for (var currComputerNumber = 0;
                ;
                currComputerNumber = ++currComputerNumber % computers.Count)
            {
                var currComputer = computers[currComputerNumber];
                if (lastOutput.HasValue)
                {
                    currComputer.AddInputValue(lastOutput.Value);
                }

                var programToRun = currComputer.HasLoadedProgram ?
                    null : Program;

                currComputer.RunProgram(programToRun, true);

                lastOutput = currComputer.OutputValue;

                if (currComputer == computers.Last()
                    && currComputer.FinishedProgram)
                {
                    break;
                }
            }

            return lastOutput.Value;
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