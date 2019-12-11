using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day10
{
    public abstract class Puzzle : IPuzzle
    {
        protected bool[][] Asteroids;
        public void LoadInput(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            Asteroids = lines.Select(l => l.Select(p => p == '#').ToArray()).ToArray();
        }

        public abstract string GetResult();

        protected Point GetVector(Point src, Point dst)
        {
            var x = dst.X - src.X;
            var y = dst.Y - src.Y;
            
            y = x == 0 && y != 0 ? 1 : y;
            x = y == 0 && x != 0 ? 1 : x;

            var divider = GreatestCommonDivider(x, y);
            x /= divider;
            y /= divider;
            return new Point(x, y);
        }

        protected int GreatestCommonDivider(int a, int b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
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