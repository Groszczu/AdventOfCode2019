using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day10
{
    public class Puzzle1 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var maxDetected = 0;
            foreach (var from in AsteroidsCoords)
            {
                var detected = 0;
                foreach (var to in AsteroidsCoords)
                {
                    if (from == to)
                    {
                        continue;
                    }
                    var vector = GetVector(from, to);
                    var blocked = false;
                    for (var pos = from + vector; !pos.Equals(to); pos += vector)
                    {
                        if (AsteroidsCoords.Contains(pos))
                        {
                            blocked = true;
                            break;
                        }
                    }
                    if (!blocked)
                    {
                        detected++;
                    }
                }
                maxDetected = Math.Max(maxDetected, detected);
            }
            return maxDetected;
        }
    }
}