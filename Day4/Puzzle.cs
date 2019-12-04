using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode.Core;

namespace AdventOfCode.Day4
{
    public abstract class Puzzle : IPuzzle
    {
        protected int _min;
        protected int _max;
        public void LoadInput(string inputPath)
        {
            var input = File.ReadAllText(inputPath);
            var pattern = new Regex(@"^(?<min>(\d+))\-(?<max>(\d+))$");

            _min = int.Parse(pattern.Match(input).Groups["min"].Value);
            _max = int.Parse(pattern.Match(input).Groups["max"].Value);
        }

        public abstract int GetResult();

        protected int[] GetArrayOfDigits(int n)
        {
            return n.ToString().Select(d => (int)Char.GetNumericValue(d)).ToArray();
        } 

        protected int GetInt(int[] digits)
        {
            var strings = digits.Select(d => d.ToString());
            return int.Parse(string.Join("", strings));
        }

        protected bool IsOrdered(int[] digits)
        {
            var sortedDigits = digits.OrderBy(d => d);
            return Enumerable.SequenceEqual(sortedDigits, digits);
        }
    }
}