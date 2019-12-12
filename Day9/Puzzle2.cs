using System;
using AdventOfCode.Core;

namespace AdventOfCode.Day9
{
    public class Puzzle2 : Puzzle
    {
        private const int InputValue = 2;
        public override string GetResult()
            => CalculateResult().ToString();

        private long CalculateResult()
        {
            var intcodeComputer = new Core.IntcodeComputer(Program);
            intcodeComputer.AddInputValue(InputValue);
            intcodeComputer.RunProgram();
            return intcodeComputer.OutputValue;
        }
    }
}