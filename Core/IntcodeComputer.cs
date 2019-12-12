using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public class IntcodeComputer
    {
        private const int MaxSize = 2048;
        public long OutputValue { get; private set; }
        public bool HasLoadedProgram => _program != null;
        public bool FinishedProgram => _instructionPointer == -1 || _instructionPointer >= _program?.Count;
        public int _relativeBaseOffset = 0;
        private int _instructionPointer = 0;

        private int _numberOfInstructions;

        private readonly Dictionary<Operation, int> _parameterCount = new Dictionary<Operation, int>
        {
            { Operation.Add, 3 },
            { Operation.Multiply, 3 },
            { Operation.Input, 1 },
            { Operation.Output, 1 },
            { Operation.JumpIfTrue, 2 },
            { Operation.JumpIfFalse, 2 },
            { Operation.LessThan, 3 },
            { Operation.Equals, 3 },
            { Operation.AdjustRelativeBase, 1 },
            { Operation.Halt, 0 },
        };

        private Queue<long> _inputValues;

        private List<long> _program;

        public IntcodeComputer(List<long> program)
        {
            LoadProgram(program);
            _inputValues = new Queue<long>();
        }

        public void LoadProgram(List<long> program)
        {
            _program = new List<long>(program);
            _numberOfInstructions = _program.Count;
            _program.AddRange(new long[MaxSize - _program.Count]);
            _instructionPointer = 0;
        }

        public void AddInputValue(long value)
        {
            _inputValues.Enqueue(value);
        }

        public int RunProgram(bool breakOnOutput = false)
        {
            if (FinishedProgram)
            {
                return _instructionPointer;
            }

            while (_instructionPointer < _numberOfInstructions)
            {
                var instruction = (int)_program[_instructionPointer];
                var op = ParseToOperation(instruction);

                if (op == Operation.Halt)
                {
                    _instructionPointer = -1;
                    return _instructionPointer;
                }

                var parameters = _program.Skip(_instructionPointer + 1)
                    .Take(_parameterCount[op])
                    .Select((p, i) => (value: p, mode: ParseToParameterMode(instruction, i)))
                    .ToList();

                var jumped = false;
                switch (op)
                {
                    case Operation.Add:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple());
                            var rhs = GetParamValue(parameters[1].ToTuple());
                            var outputIndex = GetOutputIndex(parameters[2].ToTuple());

                            _program[outputIndex] = lhs + rhs;
                            break;
                        }
                    case Operation.Multiply:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple());
                            var rhs = GetParamValue(parameters[1].ToTuple());
                            var outputIndex = GetOutputIndex(parameters[2].ToTuple());

                            _program[outputIndex] = lhs * rhs;
                            break;
                        }
                    case Operation.Input:
                        {
                            var outputIndex = GetOutputIndex(parameters[0].ToTuple());
                            _program[outputIndex] = _inputValues.Dequeue();
                            break;
                        }
                    case Operation.Output:
                        {
                            var value = GetParamValue(parameters[0].ToTuple());
                            OutputValue = value;
                            if (breakOnOutput)
                            {
                                _instructionPointer += 1 + _parameterCount[op];
                                return _instructionPointer;
                            }
                            break;
                        }
                    case Operation.JumpIfTrue:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple());

                            if (firstParam != 0)
                            {
                                var jumpToValue = (int)GetParamValue(parameters[1].ToTuple());

                                _instructionPointer = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.JumpIfFalse:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple());

                            if (firstParam == 0)
                            {
                                var jumpToValue = (int)GetParamValue(parameters[1].ToTuple());

                                _instructionPointer = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.LessThan:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple());
                            var rhs = GetParamValue(parameters[1].ToTuple());
                            var outputIndex = GetOutputIndex(parameters[2].ToTuple());

                            _program[outputIndex] = lhs < rhs ? 1 : 0;
                            break;
                        }
                    case Operation.Equals:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple());
                            var rhs = GetParamValue(parameters[1].ToTuple());
                            var outputIndex = GetOutputIndex(parameters[2].ToTuple());

                            _program[outputIndex] = lhs == rhs ? 1 : 0;
                            break;
                        }
                    case Operation.AdjustRelativeBase:
                        {
                            var value = GetParamValue(parameters[0].ToTuple());
                            _relativeBaseOffset += (int)value;

                            break;
                        }
                }

                if (!jumped)
                {
                    _instructionPointer += 1 + _parameterCount[op];
                }
            }

            return _instructionPointer;
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
            return (ParameterMode)Char.GetNumericValue(paramModes[paramModes.Length - 1 - parametrIndex]);
        }

        protected string ToFiveDigitString(int number)
        {
            return number.ToString("D" + 5);
        }

        protected long GetParamValue(Tuple<long, ParameterMode> parameter)
        {
            return parameter.Item2 switch
            {
                ParameterMode.Immediate => parameter.Item1,
                ParameterMode.Position => _program[(int)parameter.Item1],
                ParameterMode.Relative => _program[_relativeBaseOffset + (int)parameter.Item1],
                _ => throw new ArgumentException(),
            };
        }

        protected int GetOutputIndex(Tuple<long, ParameterMode> parameter)
        {
            return parameter.Item2 switch
            {
                ParameterMode.Position => (int)parameter.Item1,
                ParameterMode.Relative => (int)parameter.Item1 + _relativeBaseOffset,
                _ => throw new ArgumentException(),
            };
        }
    }
}