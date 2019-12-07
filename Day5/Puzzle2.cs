using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class Puzzle2 : Puzzle
    {
        private const int InputValue = 5;
        public override int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            IntcodeComputer = new Core.IntcodeComputer(new int[] { InputValue });
            IntcodeComputer.RunProgram(Program);
            return IntcodeComputer.OutputValue;
        }
    }
}