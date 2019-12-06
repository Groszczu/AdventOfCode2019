using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day5
{
    public enum Operation
    {
        Add = 1,
        Multiply = 2,
        Input = 3,
        Output = 4,
        JumpIfTrue = 5,
        JumpIfFalse = 6,
        LessThan = 7,
        Equals = 8,
        Halt = 99
    }

    public enum ParameterMode
    {
        Position = 0,
        Immediate = 1
    }

    public abstract class Puzzle : IPuzzle
    {
        protected Dictionary<Operation, int> ParameterCount = new Dictionary<Operation, int> 
        {
            { Operation.Add, 3 },
            { Operation.Multiply, 3 },
            { Operation.Input, 1 },
            { Operation.Output, 1 },
            { Operation.JumpIfTrue, 2 },
            { Operation.JumpIfFalse, 2 },
            { Operation.LessThan, 3 },
            { Operation.Equals, 3 },     
            { Operation.Halt, 0 }
        };

        protected List<int> Values { get; private set; }

        protected int LastOutput { get; set; } = 0;
        public void LoadInput(string inputPath)
        {
            Values = File.ReadAllText(inputPath)
                .Split(',')
                .Select(int.Parse)
                .ToList();
        }
        public abstract int GetResult();

        protected void Output(int value)
        {
            LastOutput = value;
            System.Console.WriteLine(value);
        }

        protected Operation ParseToOperation(int number)
        {
            var numToStr = ToFiveDigitString(number);
            var tail = numToStr.Substring(numToStr.Length - 2);
            return (Operation)int.Parse(tail);
        }

        protected ParameterMode ParseToParameterMode(int number, int parametrIndex)
        {
            var numToStr = ToFiveDigitString(number);
            var paramModes = numToStr.Substring(0, 3);
            return (ParameterMode)Char.GetNumericValue(paramModes[paramModes.Length-1 - parametrIndex]);
        }

        protected string ToFiveDigitString(int number)
        {
            return number.ToString("D" + 5);
        }

        protected int GetParamValue(Tuple<int, ParameterMode> parameter, List<int> source)
        {
            return parameter.Item2 == ParameterMode.Immediate ?
                parameter.Item1 : source[parameter.Item1];
        }
    }
}

