using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day5
{
    public class Puzzle1 : Puzzle
    {
        private const int InputValue = 1;
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

                var parameterCount = ParameterCount[op];
                var parameters = copyOfValues.Skip(i + 1)
                    .Take(parameterCount)
                    .Select((p, i) => (value: p, mode: ParseToParameterMode(instruction, i)))
                    .ToList();

                i += 1 + parameterCount;

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
                }
            }

            return _lastOutput;
        }
    }
}