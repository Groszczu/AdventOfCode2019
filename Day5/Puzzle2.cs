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
            var copyOfValues = new List<int>(_values);
            for (var i = 0; i < copyOfValues.Count();)
            {
                var instruction = copyOfValues[i];
                var op = ParseToOperation(instruction);

                if (op == Operation.Halt)
                {
                    break;
                }

                var parameters = copyOfValues.Skip(i + 1)
                    .Take(ParameterCount[op])
                    .Select((p, i) => (value: p, mode: ParseToParameterMode(instruction, i)))
                    .ToList();

                var jumped = false;
                switch (op)
                {
                    case Operation.Add:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), copyOfValues);
                            var rhs = GetParamValue(parameters[1].ToTuple(), copyOfValues);
                            var outputIndex = parameters[2].value;

                            copyOfValues[outputIndex] = lhs + rhs;
                            break;
                        }
                    case Operation.Multiply:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), copyOfValues);
                            var rhs = GetParamValue(parameters[1].ToTuple(), copyOfValues);
                            var outputIndex = parameters[2].value;

                            copyOfValues[outputIndex] = lhs * rhs;
                            break;
                        }
                    case Operation.Input:
                        {
                            var outputIndex = parameters[0].value;
                            copyOfValues[outputIndex] = InputValue;
                            break;
                        }
                    case Operation.Output:
                        {
                            var value = GetParamValue(parameters[0].ToTuple(), copyOfValues);
                            Output(value);
                            break;
                        }
                    case Operation.JumpIfTrue:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple(), copyOfValues);

                            if (firstParam != 0)
                            {
                                var jumpToValue = GetParamValue(parameters[1].ToTuple(), copyOfValues);

                                i = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.JumpIfFalse:
                        {
                            var firstParam = GetParamValue(parameters[0].ToTuple(), copyOfValues);

                            if (firstParam == 0)
                            {
                                var jumpToValue = GetParamValue(parameters[1].ToTuple(), copyOfValues);

                                i = jumpToValue;
                                jumped = true;
                            }

                            break;
                        }
                    case Operation.LessThan:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), copyOfValues);
                            var rhs = GetParamValue(parameters[1].ToTuple(), copyOfValues);
                            var outputIndex = parameters[2].value;

                            copyOfValues[outputIndex] = lhs < rhs ? 1 : 0;
                            break;
                        }
                    case Operation.Equals:
                        {
                            var lhs = GetParamValue(parameters[0].ToTuple(), copyOfValues);
                            var rhs = GetParamValue(parameters[1].ToTuple(), copyOfValues);
                            var outputIndex = parameters[2].value;

                            copyOfValues[outputIndex] = lhs == rhs ? 1 : 0;
                            break;
                        }

                }

                if (!jumped)
                {
                    i += 1 + ParameterCount[op];
                }
            }

            return _lastOutput;
        }
    }
}