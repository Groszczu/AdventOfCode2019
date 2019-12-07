using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day7
{
    public abstract class Puzzle : IPuzzle
    {
        protected List<int> Program { get; private set; }

        public void LoadInput(string inputPath)
        {
            Program = File.ReadAllText(inputPath)
                .Split(',')
                .Select(int.Parse)
                .ToList();
        }
        public abstract int GetResult();

        protected static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1) return list.Select(t => new T[] { t });
            return GetPermutations(list, length - 1)
                .SelectMany(t => list.Where(o => !t.Contains(o)),
                    (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}