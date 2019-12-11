using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day3
{
    public class Segment
    {
        public Point P1 { get; set; }
        public Point P2 { get; set; }

        public int Length => (int)Math.Sqrt(Math.Pow(P1.X - P2.X, 2) + Math.Pow(P1.Y - P2.Y, 2)); 

        public Segment(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public static Segment FromIEnumerable(IEnumerable<Point> points)
        {
            return new Segment(points.First(), points.Skip(1).First());
        }
    }
}