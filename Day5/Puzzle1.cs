using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class Puzzle1 : Puzzle
    {
        private const int InputValue = 1;
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