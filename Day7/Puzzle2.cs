using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day7
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

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
                var computer = new IntcodeComputer(new long[] { setting });
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

                lastOutput = (int)currComputer.OutputValue;

                if (currComputer == computers.Last()
                    && currComputer.FinishedProgram)
                {
                    break;
                }
            }

            return lastOutput.Value;
        }
    }
}