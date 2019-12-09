using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day2
{
    enum Operation
    {
        Add = 1,
        Multiply = 2,
        Halt = 99
    };

    public class Puzzle1 : IPuzzle
    {
        private List<int> _values;
        public void LoadInput(string inputPath)
        {
            _values = File.ReadAllText(inputPath)
                        .Split(',')
                        .Select(int.Parse)
                        .ToList();
        }
        public string GetResult()
            => CalculateResult(12, 2).ToString();

        private int CalculateResult(int noun, int verb)
        {
            var copyOfValues = new List<int>(_values);
            copyOfValues[1] = noun;
            copyOfValues[2] = verb;
            for (var i = 0; i < copyOfValues.Count(); i += 4)
            {
                var op = (Operation)copyOfValues[i];
                if (op == Operation.Halt)
                {
                    break;
                }

                var input1Index = copyOfValues[i + 1];
                var input2Index = copyOfValues[i + 2];
                var input1 = copyOfValues[input1Index];
                var input2 = copyOfValues[input2Index];
                var outputIndex = copyOfValues[i + 3];

                switch (op)
                {
                    case Operation.Add:
                        copyOfValues[outputIndex] = input1 + input2;
                        break;
                    case Operation.Multiply:
                        copyOfValues[outputIndex] = input1 * input2;
                        break;
                }
            }
            return copyOfValues[0];
        }
    }
}