using System;
using AdventOfCode.Core;

namespace AdventOfCode.Day9
{
    public class Puzzle1 : Puzzle
    {
        private const int InputValue = 1;
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