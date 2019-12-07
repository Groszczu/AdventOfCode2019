using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Core
{
    public class IntcodeComputer
    {
        public int OutputValue { get; private set; }
        public bool HasLoadedProgram => _program != null;
        public bool FinishedProgram => _instructionPointer == -1 || _instructionPointer >= _program?.Count;
        private int _instructionPointer = 0;
    
        private Dictionary<Operation, int> _parameterCount = new Dictionary<Operation, int> 
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

        private Queue<int> _inputValues;

        private List<int> _program;

        public IntcodeComputer(IEnumerable<int> input)
        {
            _inputValues = new Queue<int>(input);
        }

        public void AddInputValue(int value)
        {
            _inputValues.Enqueue(value);
        }

        public int RunProgram(List<int> program = null, bool brakeOnOutput = false)
        {
            if (program != null)
            {
                _program = new List<int>(program);
                _instructionPointer = 0;
            }

            while (_instructionPointer < _program.Count)
            {
                var instruction = _program[_instructionPointer];
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
                            var lhs = GetParamValue(parameters[0].ToTuple(), _program);
                            var rhs = GetParamValue(parameters[1].ToTuple(), _program);
                            var outputIndex = parameters[2].value;

                            _program[outputIndex] = lhs + rhs;
                            break;
                        }
                    case Operation.Multiply:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), _program);
                            var rhs = GetParamValue(parameters[1].ToTuple(), _program);
                            var outputIndex = parameters[2].value;

                            _program[outputIndex] = lhs * rhs;
                            break;
                        }
                    case Operation.Input:
                        {
                            var outputIndex = parameters[0].value;
                            _program[outputIndex] = _inputValues.Dequeue();
                            break;
                        }
                    case Operation.Output:
                        {
                            var value = GetParamValue(parameters[0].ToTuple(), _program);
                            OutputValue = value;
                            if (brakeOnOutput)
                            {
                                _instructionPointer += 1 + _parameterCount[op];
                                return _instructionPointer;
                            }
                            break;
                        }
                    case Operation.JumpIfTrue:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple(), _program);

                            if (firstParam != 0)
                            {
                                var jumpToValue = GetParamValue(parameters[1].ToTuple(), _program);

                                _instructionPointer = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.JumpIfFalse:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple(), _program);

                            if (firstParam == 0)
                            {
                                var jumpToValue = GetParamValue(parameters[1].ToTuple(), _program);

                                _instructionPointer = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.LessThan:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), _program);
                            var rhs = GetParamValue(parameters[1].ToTuple(), _program);
                            var outputIndex = parameters[2].value;

                            _program[outputIndex] = lhs < rhs ? 1 : 0;
                            break;
                        }
                    case Operation.Equals:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), _program);
                            var rhs = GetParamValue(parameters[1].ToTuple(), _program);
                            var outputIndex = parameters[2].value;

                            _program[outputIndex] = lhs == rhs ? 1 : 0;
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