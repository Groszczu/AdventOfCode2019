using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class Puzzle2 : Puzzle
    {
        private const int InputValue = 5;
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            IntcodeComputer = new Core.IntcodeComputer(new long[] { InputValue });
            IntcodeComputer.RunProgram(Program);
            return (int)IntcodeComputer.OutputValue;
        }
    }
}