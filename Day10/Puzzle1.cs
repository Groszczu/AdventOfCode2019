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
            var asteroidsCoords = new HashSet<Point>();
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    if (Asteroids[row][column])
                    {
                        asteroidsCoords.Add(new Point(column, row));
                    }
                }
            }

            var maxDetected = 0;
            foreach (var from in asteroidsCoords)
            {
                var detected = 0;
                foreach (var to in asteroidsCoords)
                {
                    if (from == to)
                    {
                        continue;
                    }
                    var vector = GetVector(from, to);
                    var blocked = false;
                    for (var pos = from + vector; !pos.Equals(to); pos += vector)
                    {
                        if (asteroidsCoords.Contains(pos))
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