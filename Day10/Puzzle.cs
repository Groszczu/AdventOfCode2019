using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using AdventOfCode.Core;

namespace AdventOfCode.Day10
{
    public abstract class Puzzle : IPuzzle
    {
        protected bool[][] Asteroids;
        protected HashSet<Point> AsteroidsCoords { get; private set; }
        protected int Rows => Asteroids.Count();
        protected int Columns => Asteroids[0].Count();
        public void LoadInput(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            Asteroids = lines.Select(l => l.Select(p => p == '#').ToArray()).ToArray();

            AsteroidsCoords = new HashSet<Point>();
            for (var row = 0; row < Rows; row++)
            {
                for (var column = 0; column < Columns; column++)
                {
                    if (Asteroids[row][column])
                    {
                        AsteroidsCoords.Add(new Point(column, row));
                    }
                }
            }
        }

        public abstract string GetResult();

        protected int DetectedAsteroidsCount(Point from)
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

            return detected;
        }

        protected Point GetVector(Point src, Point dst)
        {
            var result = dst - src;

            var divisor = (int)Gcd(result.X, result.Y);
            result.X /= divisor;
            result.Y /= divisor;
            return result;
        }

        protected Point GetUnscaledVector(Point src, Point dst)
        {
            return dst - src;
        }

        public static long Gcd(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            if (a == 0)
            {
                return b != 0 ? b : 1;
            }
            if (b == 0)
            {
                return a != 0 ? a : 1;
            }
            while (b != 0)
            {
                var c = a % b;
                a = b;
                b = c;
            }

            return a == 0 ? 1 : a;
        }
    }
}