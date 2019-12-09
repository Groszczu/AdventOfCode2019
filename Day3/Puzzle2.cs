using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day3
{
    public class Puzzle2 : Puzzle
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

            var wire1SegmentsWithDistance = wire1Segments
                .Select(
                    (s, i) => 
                        (segment: s,
                        distance: wire1Segments.Take(i+1)
                        .Select(x => x.Length)
                        .Sum())
                )
                .ToDictionary(p => p.segment, p => p.distance);

            var wire2SegmentsWithDistance = wire2Segments
                .Select(
                    (s, i) => 
                        (segment: s,
                        distance: wire2Segments.Take(i+1)
                        .Select(x => x.Length)
                        .Sum())
                )
                .ToDictionary(p => p.segment, p => p.distance);

            var combinedDistances = intersections.Select(i =>
                {
                    var w1 = wire1SegmentsWithDistance.First(s => Contains(s.Key, i));
                    var w2 = wire2SegmentsWithDistance.First(s => Contains(s.Key, i));

                    var w1DistanceToPoint = w1.Value - w1.Key.Length + Distance(w1.Key.P1, i);
                    var w2DistanceToPoint = w2.Value - w2.Key.Length + Distance(w2.Key.P1, i);

                    return w1DistanceToPoint + w2DistanceToPoint;
                }
            );

            return combinedDistances.Min();
        }
        private bool Contains(Segment s, Point p)
        {
            if (IsVerticalSegment(s))
            {
                var verticalMaxHeight = Math.Max(s.P1.Y, s.P2.Y);
                var verticalMinHeight = Math.Min(s.P1.Y, s.P2.Y);

                return p.X == s.P1.X && p.Y >= verticalMinHeight && p.Y <= verticalMaxHeight;
            }

            if (IsHorizontalSegment(s))
            {
                var horizontalMaxWidth = Math.Max(s.P1.X, s.P2.X);
                var horizontalMinWidth = Math.Min(s.P1.X, s.P2.X);

                return p.Y == s.P1.Y && p.X >= horizontalMinWidth && p.X <= horizontalMaxWidth;
            }

            return false;
        }

        private int Distance(Point p1, Point p2)
        {
            return (int)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2)); 
        }
    }
}