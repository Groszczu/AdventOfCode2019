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
            var rows = Asteroids.Count();
            var columns = Asteroids[0].Count();
            var maxDetected = 0;
            for (var row = 0; row < rows; row++)
            {
                for (var column = 0; column < columns; column++)
                {
                    if (!Asteroids[row][column])
                    {
                        continue;
                    }

                    var currCoordinates = new Point(column, row);
                    var detected = 1;
                    var vectorsBlocked = new HashSet<Point>();
                    for (var r = 0; r < rows; r++)
                    {
                        for (var c = 0; c < columns; c++)
                        {
                            if (!Asteroids[r][c])
                            {
                                continue;
                            }

                            var vector = GetVector(currCoordinates, new Point(c, r));

                            if (!vectorsBlocked.Contains(vector))
                            {
                                detected++;
                                vectorsBlocked.Add(vector);
                            }
                        }
                    }

                    maxDetected = Math.Max(maxDetected, detected);
                }
            }

            return maxDetected;
        }
    }
}