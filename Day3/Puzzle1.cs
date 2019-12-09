using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Puzzle1 : Puzzle
    {
        override public string GetResult()
            => CalculateResult().ToString();

        private int CalculateResult()
        {
            var wire1Segments = GetSegments(Wire1Commands);
            var wire2Segments = GetSegments(Wire2Commands);

            var intersections = new List<Point>();
            foreach (var segment1 in wire1Segments)
            {
                intersections.AddRange(
                    GetIntersectionPoints(
                        segment1, 
                        wire2Segments
                    )
                );
            }
            
            return intersections.Min(p => Math.Abs(p.X) + Math.Abs(p.Y));
        }
    }
}