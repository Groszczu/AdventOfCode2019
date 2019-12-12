using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day10
{
    public class Puzzle2 : Puzzle
    {
        public override string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var monitoringStation = AsteroidsCoords.OrderByDescending(a => DetectedAsteroidsCount(a))
            .First();

            var pointsByAngle = AsteroidsCoords.Where(p => !p.Equals(monitoringStation))
                .OrderBy(p => p.ManhattanDistance(monitoringStation))
                .GroupBy(p => p.GetAngle(monitoringStation))
                .ToDictionary(g => g.Key, g => g.ToList());
            var angles = pointsByAngle.Keys.OrderBy(a => a).ToArray();

            var destroyedPoints = new Stack<Point>();

            for (var i = 0; destroyedPoints.Count != 200; i++)
            {
                var points = pointsByAngle[angles[i % angles.Count()]];
                if (points.Count != 0)
                {
                    var first = points.First();
                    destroyedPoints.Push(first);
                    points.Remove(first);
                }
            }

            return destroyedPoints.Peek().X * 100 + destroyedPoints.Peek().Y;
        }
    }
}