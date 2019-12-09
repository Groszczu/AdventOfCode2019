using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day3
{
    public abstract class Puzzle : IPuzzle
    {
        protected List<string> Wire1Commands { get; private set; }
        protected List<string> Wire2Commands { get; private set; }
        public void LoadInput(string inputPath)
        {
            var fileReader = new StreamReader(inputPath);
            Wire1Commands = fileReader.ReadLine().Split(',').ToList();
            Wire2Commands = fileReader.ReadLine().Split(',').ToList();
            fileReader.Close();
        }

        public abstract string GetResult();

        protected List<Segment> GetSegments(List<string> commands)
        {
            var edges = new List<Point> { new Point(0, 0) };
            var point = new Point(0, 0);

            edges.AddRange(
                commands.Select(command =>
                {
                    var destination = command[0];
                    var value = int.Parse(command.Substring(1));
                    switch (destination)
                    {
                        case 'L':
                            point.X += -1 * value;
                            break;
                        case 'R':
                            point.X += value;
                            break;
                        case 'U':
                            point.Y += value;
                            break;
                        case 'D':
                            point.Y += -1 * value;
                            break;
                        default:
                            throw new InvalidDataException();
                    }

                    return new Point(point);
                })
            );

            var segments = edges.Take(edges.Count() - 1)
                .Select((e, i) => new Segment(e, edges[i + 1]))
                .ToList();

            return segments;
        }

        protected bool Intersects(Segment s1, Segment s2)
        {
            Segment vertical = null;
            Segment horizontal = null;
            if (IsVerticalSegment(s1))
            {
                vertical = s1;
                if (IsHorizontalSegment(s2))
                {
                    horizontal = s2;
                }
            }
            else if (IsVerticalSegment(s2))
            {
                vertical = s2;
                if (IsHorizontalSegment(s1))
                {
                    horizontal = s1;
                }

            }
            if (vertical == null || horizontal == null)
            {
                return false;
            }

            var verticalMaxHeight = Math.Max(vertical.P1.Y, vertical.P2.Y);
            var verticalMinHeight = Math.Min(vertical.P1.Y, vertical.P2.Y);

            var horizontalMaxWidth = Math.Max(horizontal.P1.X, horizontal.P2.X);
            var horizontalMinWidth = Math.Min(horizontal.P1.X, horizontal.P2.X);

            return vertical.P1.X >= horizontalMinWidth && vertical.P1.X <= horizontalMaxWidth
                && horizontal.P1.Y >= verticalMinHeight && horizontal.P1.Y <= verticalMaxHeight;
        }

        protected bool IsVerticalSegment(Segment s)
        {
            return s.P1.X == s.P2.X;
        }

        protected bool IsHorizontalSegment(Segment s)
        {
            return s.P1.Y == s.P2.Y;
        }

        protected bool ArePerpendicular(Segment s1, Segment s2)
        {
            return (IsVerticalSegment(s1) && IsHorizontalSegment(s2))
                || (IsHorizontalSegment(s1) && IsVerticalSegment(s2));
        }

        protected IEnumerable<Point> GetIntersectionPoints(Segment s1, IEnumerable<Segment> segments)
        {
            return segments.Where(s => ArePerpendicular(s1, s) && Intersects(s1, s))
                .Select(s => GetIntersectionPoint(s1, s));
        }
        protected Point GetIntersectionPoint(Segment s1, Segment s2)
        {
            Segment vertical = null;
            Segment horizontal = null;
            if (IsVerticalSegment(s1))
            {
                vertical = s1;
                if (IsHorizontalSegment(s2))
                {
                    horizontal = s2;
                }
            }
            else if (IsVerticalSegment(s2))
            {
                vertical = s2;
                if (IsHorizontalSegment(s1))
                {
                    horizontal = s1;
                }
            }

            return new Point(vertical.P1.X, horizontal.P1.Y);
        }
    }
}