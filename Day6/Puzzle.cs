using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AdventOfCode.Core;

namespace AdventOfCode.Day6
{
    public abstract class Puzzle : IPuzzle
    {
        protected SpaceObjectsTree ObjectsTree { get; private set; } = new SpaceObjectsTree();
        public void LoadInput(string inputPath)
        {
            foreach (var line in File.ReadAllLines(inputPath).ToHashSet())
            {
                ObjectsTree.AddOrbiterFromString(line);
            }
        }

        public abstract int GetResult();
    }

}