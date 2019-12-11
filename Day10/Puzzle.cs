using System;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day10
{
    public abstract class Puzzle : IPuzzle
    {
        protected bool[][] Asteroids;
        protected int Rows => Asteroids.Count();
        protected int Columns => Asteroids[0].Count();
        public void LoadInput(string inputPath)
        {
            var lines = File.ReadAllLines(inputPath);
            Asteroids = lines.Select(l => l.Select(p => p == '#').ToArray()).ToArray();
        }

        public abstract string GetResult();

        protected Point GetVector(Point src, Point dst)
        {
            var result = dst - src;

            var divider = GreatestCommonDivider(result.X, result.Y);
            result.X /= divider;
            result.Y /= divider;
            return result;
        }

        protected int GreatestCommonDivider(int a, int b)
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