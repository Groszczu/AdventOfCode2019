using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode.Day12
{
    public abstract class Puzzle : IPuzzle
    {
        protected List<Moon> Moons { get; } = new List<Moon>();

        protected int TotalEnergy => Moons.Sum(m => m.TotalEnergy);
        public void LoadInput(string inputPath)
        {
            var pattern = new Regex(@"^<x=(?<x>-?\d+), y=(?<y>-?\d+), z=(?<z>-?\d+)>$");
            foreach (var line in File.ReadAllLines(inputPath))
            {
                if (pattern.IsMatch(line))
                {
                    var match = pattern.Match(line);
                    var x = int.Parse(match.Groups["x"].Value);
                    var y = int.Parse(match.Groups["y"].Value);
                    var z = int.Parse(match.Groups["z"].Value);

                    Moons.Add(new Moon(x, y, z));
                }
            }
        }

        public abstract string GetResult();

        protected void SimulateGravity()
        {
            foreach (var moon in Moons)
            {
                foreach (var other in Moons.Where(m => m != moon))
                {
                    var xComparision = other.Position.X.CompareTo(moon.Position.X);
                    var yComparision = other.Position.Y.CompareTo(moon.Position.Y);
                    var zComparision = other.Position.Z.CompareTo(moon.Position.Z);

                    moon.Velocity += new Point3D(xComparision, yComparision, zComparision);
                }
            }
        }

        protected void SimulateSteps(int numberOfSteps)
        {
            while (numberOfSteps-- > 0)
            {
                SimulateGravity();
                Step();
            }
        }

        public static long Lcm(long a, long b)
        {
            return Math.Abs(a * b)/AdventOfCode.Day10.Puzzle.Gcd(a, b);
        }

        protected void Step()
        {
            Moons.ForEach(m => m.Step());
        }
    }
}