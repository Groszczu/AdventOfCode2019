using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Day3
{
    public class Puzzle1 : Puzzle
    {
        override public int GetResult()
            => CalculateResult();

        private int CalculateResult()
        {
            var wire1Segments = GetSegments(_wire1Commands);
            var wire2Segments = GetSegments(_wire2Commands);

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